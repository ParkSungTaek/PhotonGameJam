using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class CharPlayer : EntityBase
    {
        private List<BuffBase> _buffBases    = new List<BuffBase>(); // ������ �������ִ� ����
        private PlayerInfo     _playerInfo   = null;                    // Player ������
        private int            _weaponDataID = SystemConst.NoData;
        private Rigidbody2D    _rigidbody2D  = null;
        private WeaponBase     _weapon       = null;

        private NetworkCharacterController _networkNetwork;

        protected override SystemEnum.EntityType _EntityType => SystemEnum.EntityType.CharPlayer;

        [SerializeField]
        PlayerCharName playerDataIndex;

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
            _playerInfo = new PlayerInfo((int)playerDataIndex, _weaponDataID, _buffBases);
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

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData data))
            {
                data.direction.Normalize();
                _networkNetwork.Move(_playerInfo.GetStat(EntityStat.NMovSpd) * data.direction * Runner.DeltaTime);
            }
        }

        void Move()
        {
            Vector3 deltaDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                deltaDirection += Vector3.right;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                deltaDirection += Vector3.left;
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("jumpP" + _playerInfo.GetStat(EntityStat.NJumpP));
                //_rigidbody2D.AddForce(Vector3.up * _playerInfo.GetStat(EntityStat.NJumpP), ForceMode2D.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                _weapon?.Shot();    
            }


            //Vector3 deltaDirection = GameManager.Instance.JoystickDirection;
            Vector3 targetPosition = transform.position + deltaDirection;

            transform.position = Vector3.Lerp(transform.position, targetPosition, _playerInfo.GetStat(EntityStat.NMovSpd) * Time.deltaTime);
        }


    }
}