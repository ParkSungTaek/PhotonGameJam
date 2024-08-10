using Fusion;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static Client.SystemEnum;

namespace Client
{
    public class Player : EntityBase, IPlayerLeft
    {
        [SerializeField]
        private PlayerFace playerFaceUI;
        [SerializeField]
        private PlayerCharName playerDataIndex;
        [SerializeField]
        protected ProjectileName ProjectileEnumName; // �⺻ ����ü

        private SpriteRenderer[] spriteRenderers;
        [Networked] private TickTimer _attackCoolTime { get; set; } // ���� ��Ÿ��

        private List<ProjectileBase> _projectileList = new List<ProjectileBase>(); // �߻�� ����ü 
        private List<BuffBase> _buffBases = new List<BuffBase>();       // ������ �������ִ� ����

        private PlayerInfo _playerInfo = null;                // Player ������
        private int _weaponDataID = SystemConst.NoData;
        private Rigidbody _rigidbody = null;
        private WeaponBase _weapon = null;

        private NetworkCharacterController _networkNetwork;

        protected override SystemEnum.EntityType _EntityType => SystemEnum.EntityType.Player;

        public PlayerInfo PlayerInfo => _playerInfo;
        protected string ProjectilePath(ProjectileName projectileEnumName) => $"Prefabs/Projectile/{projectileEnumName.ToString()}";

        public GameObject _deadEffect; // �״� ����Ʈ
        public GameObject _reviveEffect; // ��Ȱ ����Ʈ


        [SerializeField]
        public Image SpeakingIndicator;

        [Networked]
        public bool isSpeaking { get; set; }

        [Networked]
        NetworkString<_16> nickName { get; set; }

        [Networked]
        int decoFace { get; set; }

        [Networked]
        int decoBody { get; set; }

        [Networked]
        int decoHair { get; set; }

        [Networked]
        int decoWeapon { get; set; }

        [Networked]
        int decoHat { get; set; }

        [Networked]
        int decoCape { get; set; }

        private void Awake()
        {
            // �⺻ ����ü
            ProjectileBase projectileBase = SetProjectile(ProjectileEnumName);
            if (projectileBase != null)
            {
                _projectileList.Add(projectileBase);
            }
            _networkNetwork = GetComponent<NetworkCharacterController>();
            _playerInfo = new PlayerInfo((int)playerDataIndex);
        }

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Debug.Log($"{playerDataIndex} ����");

            if (_playerInfo == null)
            {
                Debug.LogWarning($"Player{transform.name} �� Start ���� PlayerInfo �� ����. �ش� ��Ȳ�� ������ ��Ȳ�̶�� Waring ���Źٶ�");
                _playerInfo = new PlayerInfo();
            }
            if (_playerInfo.MyEntity == null)
            {
                _playerInfo.MyEntity = this;
            }
            Debug.Log("Start");

            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            LookAtMouse();
        }

        public void DisableAllRenderers()
        {
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.enabled = false;
            }
        }

        public void EnableAllRenderers()
        {
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.enabled = true;
            }
        }

        void SetDate(int weaponDataID = SystemConst.NoData, List<BuffBase> buffList = null)
        {
            if (weaponDataID != SystemConst.NoData)
            {
                _weaponDataID = weaponDataID;
            }
            if (_buffBases != null)
            {
                _buffBases.AddRange(buffList);
            }

        }

        // �ٹ̱� �����͸� �����մϴ�.
        public void SetDecoData(DecoType type, DecoData decoData)
        {
            if (_playerInfo != null && decoData != null)
            {
                _playerInfo.SetDecoData(type, decoData);
            }
            
        }

        // �г����� �����մϴ�.
        public void SetNickName(string name)
        {
            _playerInfo.SetNickName(name);
        }

        public override void FixedUpdateNetwork()
        {
            if (HasInputAuthority)
            {
                RPC_SetPlayerInfo(
                    MyInfoManager.Instance.GetNickName(),
                    MyInfoManager.Instance.GetDecoData()[DecoType.Face].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Body].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Hair].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Weapon].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Hat].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Cape].index,
                    isSpeaking);

                if (_playerInfo.GetStat(EntityStat.HP) <= 0.0f && _playerInfo.IsLive)
                {
                    _playerInfo.SetStat(EntityStat.HP, 0);
                    RPC_SetPlayerDead();
                    _playerInfo.IsLive = false;

                    EntityManager.Instance.MyPlayer = this;
                    Dead();
                }
            }

            if (GetInput(out NetworkInputData data))
            {
                if (_playerInfo == null)
                {
                    return;
                }

                data.movementInput.Normalize();
                _networkNetwork.Move(_playerInfo.GetStat(EntityStat.MovSpd) * data.movementInput * Runner.DeltaTime);

                if (data.isJumpPressed)
                {
                    _networkNetwork.Jump(false, _playerInfo.GetStat(EntityStat.JumpP));
                }

                // TODO �輱�� �ӽÿ�
                if (data.isChangeBullet)
                {
                    ProjectileEnumName++;
                    if(ProjectileEnumName >= ProjectileName.MaxCount)
                    {
                        ProjectileEnumName = ProjectileName.Projectile1;
                    }

                    ProjectileBase projectileBase = SetProjectile(ProjectileEnumName);
                    if (projectileBase != null)
                    {
                        _projectileList[0] = projectileBase;
                    }
                }

                if (HasStateAuthority && _attackCoolTime.ExpiredOrNotRunning(Runner))
                {
                    //  ���콺 ����Ű
                    if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
                    {
                        float attackSpeed = _playerInfo.GetStat(EntityStat.AtkSpd);
                        _attackCoolTime = TickTimer.CreateFromSeconds(Runner, attackSpeed);
                        ProjectileBase projectile = GetProjectileList();
                        if (projectile == null)
                        {
                            Debug.LogWarning("����ü�� ã�� ����");
                            return;
                        }
                        AudioManager.Instance.PlayOneShot("TmpShotSound");
                        Quaternion rotation = Quaternion.Euler(0, 0, CalculateAngle(data.lookDirection));
                        var bullet = Runner.Spawn(projectile,
                        transform.position + data.lookDirection * 1.3f,
                        rotation,
                        Object.InputAuthority,
                        (runner, o) =>
                        {
                            Vector3 mouseScreenPosition = Input.mousePosition;

                            // ���콺�� z ��ġ�� �÷��̾� ������Ʈ�� z ��ġ�� ����ϴ�.
                            // �̴� ī�޶��� ����Ʈ���� ������ ����(�÷��̾��� ����)�� ����ϵ��� �ϱ� �����Դϴ�.
                            mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

                            // ��ũ�� ��ǥ�踦 ���� ��ǥ��� ��ȯ�մϴ�.
                            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                            // �÷��̾� ������Ʈ�� ���콺 ��ġ ���� ���̸� ����մϴ�.
                            Vector3 rawDirection = mouseWorldPosition - transform.position;

                            ProjectileBase projectileBase = o.GetComponent<ProjectileBase>();
                            if (projectileBase != null)
                            {
                                projectileBase.SetDamage(_playerInfo.GetStat(EntityStat.Att));
                                projectileBase.Shot(rawDirection.normalized);
                                projectileBase.SetOwner(this);
                            }
                        });

                        CharacterController playerController = GetComponent<CharacterController>();
                        Collider projectileCollider = bullet.GetComponent<Collider>();
                        Physics.IgnoreCollision(playerController, projectileCollider);
                    }
                }

            }
        }

        public void SetWeaponBase(WeaponBase weapon)
        {
            if (weapon != null)
            {
                _weapon = weapon;
                _weapon.SetCharPlayer(this);
                _weapon.transform.parent = transform;
                _playerInfo.SetDataWeaponData(_weapon.GetWeaponData());
            }

        }

        public override void Spawned()
        {
            if (HasInputAuthority)
            {
                EntityManager.Instance.MyPlayer = this;
                Debug.Log("Spawned local player");

                if( MyInfoManager.Instance.GetDecoData().Count == 0 )
                {
                    MyInfoManager.Instance.SetDecoData(DecoType.Face, DataManager.Instance.GetData<DecoData>(0));
                    MyInfoManager.Instance.SetDecoData(DecoType.Body, DataManager.Instance.GetData<DecoData>(21));
                    MyInfoManager.Instance.SetDecoData(DecoType.Hair, DataManager.Instance.GetData<DecoData>(32));
                    MyInfoManager.Instance.SetDecoData(DecoType.Weapon, DataManager.Instance.GetData<DecoData>(36));
                    MyInfoManager.Instance.SetDecoData(DecoType.Hat, DataManager.Instance.GetData<DecoData>(29));
                    MyInfoManager.Instance.SetDecoData(DecoType.Cape, DataManager.Instance.GetData<DecoData>(25));
                }

                RPC_SetPlayerInfo(
                    MyInfoManager.Instance.GetNickName(), 
                    MyInfoManager.Instance.GetDecoData()[DecoType.Face].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Body].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Hair].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Weapon].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Hat].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Cape].index,
                    isSpeaking);
                RPC_SetPlayerHP(1);
            }
            else
            {
                Debug.Log("Spawned remote player");
            }

        }

        public void PlayerLeft(PlayerRef player)
        {
            if (player == Object.InputAuthority)
                Runner.Despawn(Object);

        }

        public override void Render()
        {
            base.Render();
        }

        public void OnDamage(float damage)
        {
            if (!HasInputAuthority)
            {
                float hp = _playerInfo.GetStat(EntityStat.HP);
                hp -= damage;
                if (hp >= 0)
                {
                    _playerInfo.SetStat(EntityStat.HP, hp);
                    // TODO �̼��� : 100�̾ƴ϶� MaxHP�־������
                    RPC_SetPlayerHP(_playerInfo.GetStat(EntityStat.HP) / 100);
                }
            }
        }

        ProjectileBase GetProjectileList()
        {
            // ������ ���� ����ü�� �ٱ� �� ����
            if (_projectileList == null || _projectileList.Count == 0)
            {
                return null;
            }
            // �ϴ��� ����Ʈ ����ü ����
            return _projectileList[0];
        }

        ProjectileBase SetProjectile(ProjectileName projectileEnumName)
        {
            ProjectileBase projectileBase = ObjectManager.Instance.Load<ProjectileBase>(ProjectilePath(ProjectileEnumName));
            if (projectileBase != null)
            {
                _projectileList.Add(projectileBase);
            }
            return projectileBase;
        }

        void LookAtMouse()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;

            // ���콺�� z ��ġ�� �÷��̾� ������Ʈ�� z ��ġ�� ����ϴ�.
            // �̴� ī�޶��� ����Ʈ���� ������ ����(�÷��̾��� ����)�� ����ϵ��� �ϱ� �����Դϴ�.
            mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

            // ��ũ�� ��ǥ�踦 ���� ��ǥ��� ��ȯ�մϴ�.
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            // �÷��̾� ������Ʈ�� ���콺 ��ġ ���� ���̸� ����մϴ�.
            Vector3 rawDirection = mouseWorldPosition - transform.position;

            GameManager.Instance.LookDirection = rawDirection.normalized;

        }
        public static float CalculateAngle(Vector3 direction)
        {
            // Z ���� �����ϰ�, X, Y ���� ����Ͽ� ������ ����մϴ�.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // X���� ��(+) ������ 0���� �����ϰ�, X���� ��(-) ������ 180���� �����ϱ� ���� ������ �����մϴ�.
            if (angle < 0)
            {
                angle += 360;
            }
            return angle;
        }

        private void Dead()
        {
            Debug.Log("����");

            DisableAllRenderers();
            DeadEffect();

            UIManager.Instance.ShowPopupUI<SelectSkillPage>();
        }

        public void ReSpawn()
        {
            Debug.Log("������");

            EnableAllRenderers();

            transform.position = new Vector3(-0.1806704f, 0.688218f, 0.0f);
            _playerInfo.SetStat(EntityStat.HP, 100f);
            _playerInfo.IsLive = true;
            RPC_SetPlayerResapwn();
        }

        //========== ����Ʈ ���� TODO �輱�� ���߿� partial class�� ���� ��Ű��
        void DeadEffect()
        {
            if (_deadEffect != null)
            {
                var daedInstance = Instantiate(_deadEffect, transform);

                //Destroy hit effects depending on particle Duration time
                var deadPs = daedInstance.GetComponent<ParticleSystem>();
                if (deadPs != null)
                {
                    Destroy(daedInstance, deadPs.main.duration);
                }
                else
                {
                    var hitPsParts = daedInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(daedInstance, hitPsParts.main.duration);
                }
            }
        }

        void ReviveEffect()
        {
            if (_reviveEffect != null)
            {
                var reviveInstance = Instantiate(_reviveEffect, transform);

                //Destroy hit effects depending on particle Duration time
                var revivePs = reviveInstance.GetComponent<ParticleSystem>();
                if (revivePs != null)
                {
                    Destroy(reviveInstance, revivePs.main.duration);
                }
                else
                {
                    var revivePsParts = reviveInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(reviveInstance, revivePsParts.main.duration);
                }
            }
        }

        //========== ��Ʈ��ũ ���� TODO �輱�� ���߿� partial class�� ���� ��Ű��
        // �÷��̾� ���� ����ȭ
        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_SetPlayerInfo(string name, int face, int body, int hair, int weapon, int hat, int cape, bool speaking, RpcInfo info = default)
        {
            nickName = name;
            decoFace = face;
            decoBody = body;
            decoHair = hair;
            decoWeapon = weapon;
            decoHat = hat;
            decoCape = cape;

            isSpeaking = speaking;

            SetNickName(nickName.ToString());
            playerFaceUI.SetNickName(nickName.ToString());
            playerFaceUI.SetPlayerDeco(DecoType.Face, DataManager.Instance.GetData<DecoData>(decoFace));
            playerFaceUI.SetPlayerDeco(DecoType.Body, DataManager.Instance.GetData<DecoData>(decoBody));
            playerFaceUI.SetPlayerDeco(DecoType.Hair, DataManager.Instance.GetData<DecoData>(decoHair));
            playerFaceUI.SetPlayerDeco(DecoType.Weapon, DataManager.Instance.GetData<DecoData>(decoWeapon));
            playerFaceUI.SetPlayerDeco(DecoType.Hat, DataManager.Instance.GetData<DecoData>(decoHat));
            playerFaceUI.SetPlayerDeco(DecoType.Cape, DataManager.Instance.GetData<DecoData>(decoCape));
            playerFaceUI.RefreshDeco();

            if (isSpeaking)
            {
                SpeakingIndicator.enabled = true;
            }
            else
            {
                SpeakingIndicator.enabled = false;
            }
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        // �÷��̾� ü�� ��ȭ ����ȭ
        public void RPC_SetPlayerHP(float value)
        {
            _playerInfo.SetStat(EntityStat.HP, value * 100f);
            playerFaceUI.SetHPBar(value);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        // �÷��̾� ���� ����ȭ
        public void RPC_SetPlayerDead()
        {
            _playerInfo.SetStat(EntityStat.HP, 0f);
            _playerInfo.IsLive = false;

            DisableAllRenderers();
            DeadEffect();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        // �÷��̾� ������ ����ȭ
        // TODO : �輱�� ���ݿ� �°� �ִ�ü�� ����
        public void RPC_SetPlayerResapwn()
        {
            _playerInfo.SetStat(EntityStat.HP, 100f);
            _playerInfo.IsLive = true;
            playerFaceUI.SetHPBar(1);

            EnableAllRenderers();
            ReviveEffect();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
        public void RPC_SendMessage(string message, RpcInfo info = default)
        {
            RPC_RelayMessage(message, info.Source);
        }
        [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
        public void RPC_RelayMessage(string message, PlayerRef messageSource)
        {

            if (messageSource == Runner.LocalPlayer)
            {
                message = $"You said: {message}\n";
            }
            else
            {
                message = $"Some other player said: {message}\n";
            }

        }


        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        // ���� ����ȭ
        public void RPC_SetBuff(int buffIndex)
        {
            BuffBase buffBase = BuffManager.Instance.SetBuff(buffIndex);
            PlayerInfo.ExecuteBuff(buffBase);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        // ���� ����ȭ
        public void RPC_SetMagicElement(int magicElement)
        {
            PlayerInfo.MagicElements.Add((SystemEnum.MagicElement)magicElement);
        }
    }
}