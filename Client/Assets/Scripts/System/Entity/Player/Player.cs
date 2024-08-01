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

        [Networked] private TickTimer _attackCoolTime { get; set; } // ���� ��Ÿ��

        private List<BuffBase> _buffBases    = new List<BuffBase>(); // ������ �������ִ� ����
        private PlayerInfo     _playerInfo   = new();                // Player ������
        private int            _weaponDataID = SystemConst.NoData;
        private Rigidbody2D    _rigidbody2D  = null;
        private WeaponBase     _weapon       = null;

        private NetworkCharacterController _networkNetwork;

    protected override SystemEnum.EntityType _EntityType => SystemEnum.EntityType.Player;

        public PlayerInfo PlayerInfo => _playerInfo;


        private void Awake()
        {
            //Test ��
            #region �׽�Ʈ�ڵ�
            {
                Debug.Log("EntityPlayer ��ũ��Ʈ�� �׽�Ʈ �ڵ尡 �������� �ʾҽ��ϴ�");
                EntityManager.Instance.MyPlayer = this;
            }
            #endregion

            _networkNetwork = GetComponent<NetworkCharacterController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //_rigidbody2D = GetComponent<Rigidbody2D>();
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
                    //  ���콺 ����Ű
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

                                // ���콺�� z ��ġ�� �÷��̾� ������Ʈ�� z ��ġ�� ����ϴ�.
                                // �̴� ī�޶��� ����Ʈ���� ������ ����(�÷��̾��� ����)�� ����ϵ��� �ϱ� �����Դϴ�.
                                mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

                                // ��ũ�� ��ǥ�踦 ���� ��ǥ��� ��ȯ�մϴ�.
                                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

                                // �÷��̾� ������Ʈ�� ���콺 ��ġ ���� ���̸� ����մϴ�.
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