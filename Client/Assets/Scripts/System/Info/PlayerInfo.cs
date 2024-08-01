using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class PlayerInfo : EntityInfo
    {
        private string                         _charName   = "Default Name"; // 닉네임
        private WeaponData                     _weaponData = null;           // Player 데이터
        private List<BuffBase>                 _buffBases  = null;           // 보유중인 버프
        private EntityPlayerData               _playerData = null;           // Player 데이터
        private Dictionary<DecoType, DecoData> _decoData   = new();          // 꾸미기 데이터
        #region BuffData

        // 버프라는건 만분률 버프 더해서 하는 버프 그걸 마지막에 Get해서 계산 한번에 순회해서 계산 
        // enpum 

        #endregion

        public string                         CharName => _charName;
        public Dictionary<DecoType, DecoData> DecoData => _decoData;

        public PlayerInfo() 
        {
            
        }
        public PlayerInfo(int playerDataID = SystemConst.NoData, int weaponDataID = SystemConst.NoData, List<BuffBase> buffBases = null, Dictionary<DecoType, DecoData>decoInfo = null)
        {
            SetData(playerDataID, weaponDataID, buffBases, decoInfo);
        }
        // 현재 캐릭터 정보, 무기 정보, 버프 리스트(전부 활성화) 정보 Set
        public void SetData(int playerDataID = SystemConst.NoData, int weaponDataID = SystemConst.NoData, List<BuffBase> buffBases = null, Dictionary<DecoType, DecoData> decoInfo = null)
        {
            if (playerDataID != SystemConst.NoData)
            {
                _playerData = DataManager.Instance.GetData<EntityPlayerData>(playerDataID);
                if (_playerData == null)
                {
                    Debug.LogWarning($"PlayerInfo {playerDataID} 에 해당하는 PlayerData 정보 찾지 못함");
                    //return;
                    // 지금은 데이터 테이블이 없다 나중에 반드시!! 제거 바람

                    _playerData = new EntityPlayerData();
                }

                EntityStatDic[EntityStat.JumpP] = _playerData._jumpPower;
                EntityStatDic[EntityStat.NJumpP] = _playerData._jumpPower;

                EntityStatDic[EntityStat.HP] = _playerData._hp;
                EntityStatDic[EntityStat.NHP] = _playerData._hp;

                EntityStatDic[EntityStat.MovSpd] = _playerData._movSpd;
                EntityStatDic[EntityStat.NMovSpd] = _playerData._movSpd;

            }
            if (weaponDataID != SystemConst.NoData)
            {
                _weaponData = DataManager.Instance.GetData<WeaponData>(weaponDataID);
                if (_weaponData == null)
                {
                    Debug.LogWarning($"PlayerInfo {weaponDataID} 에 해당하는 WeaponData 정보 찾지 못함");
                    //무기는 안끼고 있을수도?
                    // 지금은 데이터 테이블이 없다 나중에 반드시!! 제거 바람

                    _weaponData = new WeaponData();
                }
                else
                {
                    EntityStatDic[EntityStat.AtkSpd] = _weaponData._atkSpd;
                    EntityStatDic[EntityStat.NAtkSpd] = _weaponData._atkSpd;

                    EntityStatDic[EntityStat.Att] = _weaponData._Att;
                    EntityStatDic[EntityStat.NAtt] = _weaponData._Att;
                }
            }
            if (buffBases != null)
            {
                _buffBases = buffBases;
                foreach (var buff in buffBases)
                {
                    buff.Execute();
                }
            }
        }

        public void SetDataWeaponData(WeaponData weaponData)
        {
            _weaponData = weaponData;
            if (_weaponData == null)
            {
                Debug.LogWarning($"SetDataWeaponData 정보 찾지 못함");

            }
            else
            {
                EntityStatDic[EntityStat.AtkSpd] = _weaponData._atkSpd;
                EntityStatDic[EntityStat.NAtkSpd] = _weaponData._atkSpd;

                EntityStatDic[EntityStat.Att] = _weaponData._Att;
                EntityStatDic[EntityStat.NAtt] = _weaponData._Att;
            }
        }

        // 꾸미기 데이터를 세팅합니다.
        public void SetDecoData(DecoType type, DecoData decoData)
        {
            _decoData[type] = decoData;
        }

        // 닉네임을 세팅합니다.
        public void SetNickName(string name)
        {
            _charName = name;
        }

        // 단일 버프 활성화
        public void ExecuteBuff(BuffBase buff)
        {
            _buffBases.Add(buff);
            if (buff != null)
            {
                buff.Execute();
            }
        }
        // 단일 버프 비활성화
        public void DeleteBuff(BuffBase buff)
        {
            if (_buffBases.Contains(buff))
            {
                if (buff != null)
                {
                    buff.Dispel();
                }
                _buffBases.Remove(buff);
            }
        }

        #region 데이터 Get 영역 반드시 만분률에 주의해주세요!!!!!
        /// <summary>
        /// 만분률 이후 값을 Get하는 함수
        /// </summary>
        /// <returns></returns>
        public float GetStat(EntityStat entityStat)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 {entityStat.ToString()} 존재하지않음");
                return 0;
            }
            return EntityStatDic[entityStat] / SystemConst.Per;
                   
        }

        /// <summary>
        /// 만분률 이전값을 Get하는 함수
        /// </summary>
        /// <param name="entityStat"></param>
        /// <returns></returns>
        public int GetRawStat(EntityStat entityStat)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 {entityStat.ToString()} 존재하지않음");
            }
            return EntityStatDic[entityStat];

        }


        /// <summary>
        /// 일반값 (만분률 이후 값) 을 Set하는 함수 만분률값인지 반드시 확인바람
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public float SetStat(EntityStat entityStat, float data)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 {entityStat.ToString()} 존재하지않음");
            }
            EntityStatDic[entityStat] = (int)(data * SystemConst.Per);
            return EntityStatDic[entityStat] / SystemConst.Per;
        }

        /// <summary>
        /// Raw(만분률 이전값)값을 Set하는 함수 만분률값인지 반드시 확인바람
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public int SetRawStat(EntityStat entityStat, int data)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 {entityStat.ToString()} 존재하지않음");
            }
            EntityStatDic[entityStat] = data;
            return EntityStatDic[entityStat];
        }

        #endregion
    }
}