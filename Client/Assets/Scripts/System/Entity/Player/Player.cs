using Fusion;
using System.Collections;
using System.Collections.Generic;
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
        private Bullet playerBullet;
        [SerializeField] 
        protected ProjectileName ProjectileEnumName; // 기본 투사체
        
        [Networked]private TickTimer _attackCoolTime { get; set; } // 공격 쿨타임

        private List<ProjectileBase> _projectileList = new List<ProjectileBase>(); // 발사용 투사체 
        private List<BuffBase> _buffBases            = new List<BuffBase>();       // 본인이 가지고있는 버프
        
        private PlayerInfo     _playerInfo   = new();                // Player 데이터
        private int            _weaponDataID = SystemConst.NoData;
        private Rigidbody      _rigidbody    = null;
        private WeaponBase     _weapon       = null;

        private NetworkCharacterController _networkNetwork;

        protected override SystemEnum.EntityType _EntityType => SystemEnum.EntityType.Player;

        public PlayerInfo PlayerInfo => _playerInfo;
        protected string ProjectilePath(ProjectileName projectileEnumName) => $"Prefabs/Projectile/{projectileEnumName.ToString()}";


        private void Awake()
        {
            //Test 용
            #region 테스트코드
            {
                Debug.Log("EntityPlayer 스크립트의 테스트 코드가 지워지지 않았습니다");
                EntityManager.Instance.MyPlayer = this;
            }
            #endregion
            // 기본 투사체
            ProjectileBase projectileBase = SetProjectile(ProjectileEnumName);
            if (projectileBase != null)
            {
                _projectileList.Add(projectileBase);
            }
            _networkNetwork = GetComponent<NetworkCharacterController>();
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

            foreach (var data in _playerInfo.DecoData)
            {
                playerFaceUI.SetPlayerDeco(data.Key, data.Value);
            }
            playerFaceUI.SetNickName(_playerInfo.CharName);
            playerFaceUI.RefreshDeco();

            //if (EntityManager.Instance.MyPlayer == this)
            //{
            //    GameManager.Instance.AddOnUpdate(Move);
            //}
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

        // 꾸미기 데이터를 세팅합니다.
        public void SetDecoData(DecoType type, DecoData decoData)
        {
            _playerInfo.SetDecoData(type, decoData);
        }

        // 닉네임을 세팅합니다.
        public void SetNickName(string name)
        {
            _playerInfo.SetNickName(name);
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData data))
            {
                if (_playerInfo == null)
                {
                    return;
                }

                data.movementInput.Normalize();
                //_weapon.transform.localRotation = data.movementInput.Normalize();
                _networkNetwork.Move(5 * data.movementInput * Runner.DeltaTime);

                if (data.isJumpPressed)
                {
                    _networkNetwork.Jump();
                }

                if (HasStateAuthority && _attackCoolTime.ExpiredOrNotRunning(Runner))
                {
                    //  마우스 왼쪽키
                    if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
                    {
                        float attackSpeed = 0.1f;
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

                            o.GetComponent<Bullet>().Shot(rawDirection.normalized);
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
            if (Object.HasInputAuthority)
            {
                EntityManager.Instance.MyPlayer = this;
                Debug.Log("Spawned local player");

                // TODO: 임시로 넣음 뺄 예정
                UIManager.Instance.ShowSceneUI<ChatPage>();
            }
            else Debug.Log("Spawned remote player");

        }

        public void PlayerLeft(PlayerRef player)
        {
            if (player == Object.InputAuthority)
                Runner.Despawn(Object);

        }

        public void OnDamage(float damage)
        {
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

    }
}