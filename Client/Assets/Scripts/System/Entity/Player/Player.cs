using Fusion;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

        [Networked]
        NetworkString<_16> nickName { get; set; }

        [Networked]
        int decoFace { get; set; }

        [Networked]
        int decoBody { get; set; }

        private void Awake()
        {
            //Test ��
            #region �׽�Ʈ�ڵ�
            {
                Debug.Log("EntityPlayer ��ũ��Ʈ�� �׽�Ʈ �ڵ尡 �������� �ʾҽ��ϴ�");
                EntityManager.Instance.MyPlayer = this;
            }
            #endregion
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

            foreach (var data in _playerInfo.DecoData)
            {
                playerFaceUI.SetPlayerDeco(data.Key, data.Value);
            }
            playerFaceUI.SetNickName(_playerInfo.CharName);
            playerFaceUI.RefreshDeco();

        }

        private void Update()
        {
            LookAtMouse();
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
            _playerInfo.SetDecoData(type, decoData);
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
                RPC_CH_SendMessage(
                    MyInfoManager.Instance.GetNickName(),
                    MyInfoManager.Instance.GetDecoData()[DecoType.Face].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Body].index);
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
                        Runner.Spawn(projectile,
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
                            }
                        });
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
                    MyInfoManager.Instance.SetDecoData(DecoType.Body, DataManager.Instance.GetData<DecoData>(3));
                }

                RPC_CH_SendMessage(
                    MyInfoManager.Instance.GetNickName(), 
                    MyInfoManager.Instance.GetDecoData()[DecoType.Face].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Body].index);
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
            SetNickName(nickName.ToString());
            playerFaceUI.SetNickName(nickName.ToString());
            playerFaceUI.SetPlayerDeco(DecoType.Face, DataManager.Instance.GetData<DecoData>(decoFace));
            playerFaceUI.SetPlayerDeco(DecoType.Body, DataManager.Instance.GetData<DecoData>(decoBody));
            playerFaceUI.RefreshDeco();
        }

        public void OnDamage(float damage)
        {
            float hp = _playerInfo.GetStat(EntityStat.HP);
            hp -= damage;
            if (hp >= 0)
            {
                _playerInfo.SetStat(EntityStat.HP, hp);
            }
            else
            {
                _playerInfo.SetStat(EntityStat.HP, 0);
                _playerInfo.IsLive = false;
                Dead();
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
            Runner.Despawn(Object);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_CH_SendMessage(string name, int face, int body, RpcInfo info = default)
        {
            nickName = name;
            decoFace = face;
            decoBody = body;
        }
    }
}