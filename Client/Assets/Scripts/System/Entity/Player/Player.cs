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
        [SerializeField] PlayerFace     playerFaceUI;
        [SerializeField] PlayerCharName playerDataIndex;
        [SerializeField] private Bullet playerBullet;

        [Networked] private TickTimer _attackCoolTime { get; set; } // 공격 쿨타임

        private List<BuffBase> _buffBases    = new List<BuffBase>(); // 본인이 가지고있는 버프
        private PlayerInfo     _playerInfo   = new();                // Player 데이터
        private int            _weaponDataID = SystemConst.NoData;
        private Rigidbody2D    _rigidbody2D  = null;
        private WeaponBase     _weapon       = null;

        private NetworkCharacterController _networkNetwork;

    protected override SystemEnum.EntityType _EntityType => SystemEnum.EntityType.Player;

        public PlayerInfo PlayerInfo => _playerInfo;


        private void Awake()
        {
            //Test 용
            #region 테스트코드
            {
                Debug.Log("EntityPlayer 스크립트의 테스트 코드가 지워지지 않았습니다");
                EntityManager.Instance.MyPlayer = this;
            }
            #endregion

            _networkNetwork = GetComponent<NetworkCharacterController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //_rigidbody2D = GetComponent<Rigidbody2D>();
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
                _networkNetwork.Move(_playerInfo.GetStat(EntityStat.NMovSpd) * data.movementInput * Runner.DeltaTime);

                if (data.isJumpPressed)
                {
                    _networkNetwork.Jump();
                }

                if (HasStateAuthority && _attackCoolTime.ExpiredOrNotRunning(Runner))
                {
                    //  마우스 왼쪽키
                    if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
                    {
                        _attackCoolTime = TickTimer.CreateFromSeconds(Runner, 0.1f);
                        Runner.Spawn(playerBullet,
                            transform.position + data.movementInput,
                            Quaternion.LookRotation(data.movementInput),
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
    }
}