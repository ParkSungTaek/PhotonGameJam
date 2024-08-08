using Fusion;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        protected ProjectileName ProjectileEnumName; // 기본 투사체

        private SpriteRenderer[] spriteRenderers;
        [Networked] private TickTimer _attackCoolTime { get; set; } // 공격 쿨타임

        private List<ProjectileBase> _projectileList = new List<ProjectileBase>(); // 발사용 투사체 
        private List<BuffBase> _buffBases = new List<BuffBase>();       // 본인이 가지고있는 버프

        private PlayerInfo _playerInfo = null;                // Player 데이터
        private int _weaponDataID = SystemConst.NoData;
        private Rigidbody _rigidbody = null;
        private WeaponBase _weapon = null;

        private NetworkCharacterController _networkNetwork;

        protected override SystemEnum.EntityType _EntityType => SystemEnum.EntityType.Player;

        public PlayerInfo PlayerInfo => _playerInfo;
        protected string ProjectilePath(ProjectileName projectileEnumName) => $"Prefabs/Projectile/{projectileEnumName.ToString()}";

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

        private void Awake()
        {
            // 기본 투사체
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
            Debug.Log($"{playerDataIndex} 생성");

            if (_playerInfo == null)
            {
                Debug.LogWarning($"Player{transform.name} 의 Start 시점 PlayerInfo 가 없음. 해당 상황이 정상적 상황이라면 Waring 제거바람");
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

        // 꾸미기 데이터를 세팅합니다.
        public void SetDecoData(DecoType type, DecoData decoData)
        {
            if (_playerInfo != null && decoData != null)
            {
                _playerInfo.SetDecoData(type, decoData);
            }
            
        }

        // 닉네임을 세팅합니다.
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

                // TODO 김선중 임시용
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
                    //  마우스 왼쪽키
                    if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
                    {
                        float attackSpeed = _playerInfo.GetStat(EntityStat.AtkSpd);
                        _attackCoolTime = TickTimer.CreateFromSeconds(Runner, attackSpeed);
                        ProjectileBase projectile = GetProjectileList();
                        if (projectile == null)
                        {
                            Debug.LogWarning("투사체를 찾지 못함");
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

                            // 마우스의 z 위치를 플레이어 오브젝트의 z 위치와 맞춥니다.
                            // 이는 카메라의 뷰포트에서 동일한 깊이(플레이어의 깊이)를 사용하도록 하기 위함입니다.
                            mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

                            // 스크린 좌표계를 월드 좌표계로 변환합니다.
                            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                            // 플레이어 오브젝트와 마우스 위치 간의 차이를 계산합니다.
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
                    MyInfoManager.Instance.SetDecoData(DecoType.Hair, DataManager.Instance.GetData<DecoData>(3));
                    MyInfoManager.Instance.SetDecoData(DecoType.Weapon, DataManager.Instance.GetData<DecoData>(3));
                }

                RPC_SetPlayerInfo(
                    MyInfoManager.Instance.GetNickName(), 
                    MyInfoManager.Instance.GetDecoData()[DecoType.Face].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Body].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Hair].index,
                    MyInfoManager.Instance.GetDecoData()[DecoType.Weapon].index,
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
                    // TODO 이서연 : 100이아니라 MaxHP넣어줘야함
                    RPC_SetPlayerHP(_playerInfo.GetStat(EntityStat.HP) / 100);
                }
            }
        }

        ProjectileBase GetProjectileList()
        {
            // 버프로 인해 투사체가 바귈 수 있음
            if (_projectileList == null || _projectileList.Count == 0)
            {
                return null;
            }
            // 일단은 디폴트 투사체 제작
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

            // 마우스의 z 위치를 플레이어 오브젝트의 z 위치와 맞춥니다.
            // 이는 카메라의 뷰포트에서 동일한 깊이(플레이어의 깊이)를 사용하도록 하기 위함입니다.
            mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

            // 스크린 좌표계를 월드 좌표계로 변환합니다.
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            // 플레이어 오브젝트와 마우스 위치 간의 차이를 계산합니다.
            Vector3 rawDirection = mouseWorldPosition - transform.position;

            GameManager.Instance.LookDirection = rawDirection.normalized;

        }
        public static float CalculateAngle(Vector3 direction)
        {
            // Z 값을 무시하고, X, Y 값을 사용하여 각도를 계산합니다.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // X축의 양(+) 방향을 0도로 설정하고, X축의 음(-) 방향을 180도로 설정하기 위해 각도를 조정합니다.
            if (angle < 0)
            {
                angle += 360;
            }
            return angle;
        }

        private void Dead()
        {
            Debug.Log("죽음");

            DisableAllRenderers();

            UIManager.Instance.ShowPopupUI<SelectSkillPage>();
        }

        public void ReSpawn()
        {
            Debug.Log("리스폰");

            EnableAllRenderers();

            transform.position = new Vector3(-0.1806704f, 0.688218f, 0.0f);
            _playerInfo.SetStat(EntityStat.HP, 100f);
            _playerInfo.IsLive = true;
            RPC_SetPlayerResapwn();
        }


        // 플레이어 정보 동기화
        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_SetPlayerInfo(string name, int face, int body, int hair, int weapon, bool speaking, RpcInfo info = default)
        {
            nickName = name;
            decoFace = face;
            decoBody = body;
            decoHair = hair;
            decoWeapon = weapon;

            isSpeaking = speaking;

            SetNickName(nickName.ToString());
            playerFaceUI.SetNickName(nickName.ToString());
            playerFaceUI.SetPlayerDeco(DecoType.Face, DataManager.Instance.GetData<DecoData>(decoFace));
            playerFaceUI.SetPlayerDeco(DecoType.Body, DataManager.Instance.GetData<DecoData>(decoBody));
            playerFaceUI.SetPlayerDeco(DecoType.Body, DataManager.Instance.GetData<DecoData>(decoHair));
            playerFaceUI.SetPlayerDeco(DecoType.Body, DataManager.Instance.GetData<DecoData>(decoWeapon));
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
        // 플레이어 체력 변화 동기화
        public void RPC_SetPlayerHP(float value)
        {
            _playerInfo.SetStat(EntityStat.HP, value * 100f);
            playerFaceUI.SetHPBar(value);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        // 플레이어 죽음 동기화
        public void RPC_SetPlayerDead()
        {
            _playerInfo.SetStat(EntityStat.HP, 0f);
            _playerInfo.IsLive = false;

            DisableAllRenderers();
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        // 플레이어 리스폰 동기화
        // TODO : 김선중 스텟에 맞게 최대체력 세팅
        public void RPC_SetPlayerResapwn()
        {
            _playerInfo.SetStat(EntityStat.HP, 100f);
            _playerInfo.IsLive = true;
            playerFaceUI.SetHPBar(1);

            EnableAllRenderers();
        }
    }
}