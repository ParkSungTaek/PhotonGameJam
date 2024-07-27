using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class EntityPlayer : EntityBase
    {
        private List<BuffBase> _buffBases = new List<BuffBase>(); // 본인이 가지고있는 버프
        private PlayerInfo _playerInfo = null;                    // Player 데이터
        private int _weaponDataID = SystemConst.NoData;
        private Rigidbody2D rigidbody2D = null;

        [SerializeField]
        PlayerDataIndex playerDataIndex;

        private void Awake()
        {
            //Test 용
            #region 테스트코드
            {
                Debug.Log("EntityPlayer 스크립트의 테스트 코드가 지워지지않았습니다");
                EntityManager.Instance.MyPlayer = this;
            }
            #endregion
        }

        // Start is called before the first frame update
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            _playerInfo = new PlayerInfo((int)playerDataIndex, _weaponDataID, _buffBases);
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

            if (EntityManager.Instance.MyPlayer == this)
            {
                GameManager.Instance.AddOnUpdate(Move);
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
                rigidbody2D.AddForce(Vector3.up * _playerInfo.GetStat(EntityStat.NJumpP), ForceMode2D.Impulse);
            }


            //Vector3 deltaDirection = GameManager.Instance.JoystickDirection;
            Vector3 targetPosition = transform.position + deltaDirection;

            Debug.Log($"Speed {_playerInfo.GetStat(EntityStat.NMovSpd)*Time.deltaTime}");
            transform.position = Vector3.Lerp(transform.position, targetPosition, _playerInfo.GetStat(EntityStat.NMovSpd) * Time.deltaTime);
        }
    }
}