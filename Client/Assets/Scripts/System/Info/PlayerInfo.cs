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
        public List<MagicElement> MagicElements { get; set; } = new List<MagicElement>();

        #region BuffData

        // 버프라는건 만분률 버프 더해서 하는 버프 그걸 마지막에 Get해서 계산 한번에 순회해서 계산 
        // enpum 

        #endregion

        public string                         CharName => _charName;
        public Dictionary<DecoType, DecoData> DecoData => _decoData;

        // 생존여부
        public bool IsLive { get; set; } = true;
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

                EntityStatDic[EntityStat.HP] = _playerData._HP;
                NowStatDic[EntityStat.HP]    = _playerData._HP;
                BuffStatDic[EntityStat.HP]   = 0;

                EntityStatDic[EntityStat.MovSpd] = _playerData._Speed;
                NowStatDic[EntityStat.MovSpd]    = _playerData._Speed;
                BuffStatDic[EntityStat.MovSpd]   = 0;

                EntityStatDic[EntityStat.Att] = _playerData._Attack;
                NowStatDic[EntityStat.Att]    = _playerData._Attack;
                BuffStatDic[EntityStat.Att]   = 0;

                EntityStatDic[EntityStat.Def] = _playerData._Def;
                NowStatDic[EntityStat.Def]    = _playerData._Def;
                BuffStatDic[EntityStat.Def]   = 0;

                EntityStatDic[EntityStat.AtkSpd] = _playerData._AttackSpeed;
                NowStatDic[EntityStat.AtkSpd]    = _playerData._AttackSpeed;
                BuffStatDic[EntityStat.AtkSpd]   = 0;

                EntityStatDic[EntityStat.JumpP] = _playerData._JumpPower;
                NowStatDic[EntityStat.JumpP]    = _playerData._JumpPower;
                BuffStatDic[EntityStat.JumpP]   = 0;

                EntityStatDic[EntityStat.Reload] = _playerData._Reload;
                NowStatDic[EntityStat.Reload]    = _playerData._Reload;
                BuffStatDic[EntityStat.Reload]   = 0;
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
            if (!NowStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Now {entityStat.ToString()} 존재하지않음");
                return 0;
            }
            return NowStatDic[entityStat] / SystemConst.Per;
                   
        }

        /// <summary>
        /// 만분률 이전값을 Get하는 함수
        /// </summary>
        /// <param name="entityStat"></param>
        /// <returns></returns>
        public int GetRawStat(EntityStat entityStat)
        {
            if (!NowStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Now {entityStat.ToString()} 존재하지않음");
            }
            return NowStatDic[entityStat];

        }

        /// <summary>
        /// 만분률 이후 값을 Get하는 함수
        /// </summary>
        /// <returns></returns>
        public float GetEntityStat(EntityStat entityStat)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Entity {entityStat.ToString()} 존재하지않음");
                return 0;
            }
            return EntityStatDic[entityStat] / SystemConst.Per;

        }

        /// <summary>
        /// 만분률 이전값을 Get하는 함수
        /// </summary>
        /// <param name="entityStat"></param>
        /// <returns></returns>
        public int GetEntityRawStat(EntityStat entityStat)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Entity {entityStat.ToString()} 존재하지않음");
            }
            return EntityStatDic[entityStat];

        }

        public float GetBuffStat(EntityStat entityStat)
        {
            if (!BuffStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Buff {entityStat.ToString()} 존재하지않음");
                return 0;
            }
            return BuffStatDic[entityStat] / SystemConst.Per;

        }

        /// <summary>
        /// 만분률 이전값을 Get하는 함수
        /// </summary>
        /// <param name="entityStat"></param>
        /// <returns></returns>
        public int GetBuffRawStat(EntityStat entityStat)
        {
            if (!BuffStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Buff {entityStat.ToString()} 존재하지않음");
            }
            return BuffStatDic[entityStat];

        }

        /// <summary>
        /// 일반값 (만분률 이후 값) 을 Set하는 함수 만분률값인지 반드시 확인바람
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public float SetStat(EntityStat entityStat, float data)
        {
            if (!NowStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Now {entityStat.ToString()} 존재하지않음");
            }
            NowStatDic[entityStat] = (int)(data * SystemConst.Per);
            return NowStatDic[entityStat] / SystemConst.Per;
        }

        /// <summary>
        /// Raw(만분률 이전값)값을 Set하는 함수 만분률값인지 반드시 확인바람
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public int SetRawStat(EntityStat entityStat, int data)
        {
            if (!NowStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Now {entityStat.ToString()} 존재하지않음");
            }
            NowStatDic[entityStat] = data;
            return NowStatDic[entityStat];
        }

        /// <summary>
        /// 일반값 (만분률 이후 값) 을 Set하는 함수 만분률값인지 반드시 확인바람
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public float SetEntityStat(EntityStat entityStat, float data)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Entity {entityStat.ToString()} 존재하지않음");
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
        public int SetEntityRawStat(EntityStat entityStat, int data)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Entity {entityStat.ToString()} 존재하지않음");
            }
            EntityStatDic[entityStat] = data;
            return EntityStatDic[entityStat];
        }

        /// <summary>
        /// 일반값 (만분률 이후 값) 을 Set하는 함수 만분률값인지 반드시 확인바람
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public float SetBuffStat(EntityStat entityStat, float data)
        {
            if (!BuffStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Buff {entityStat.ToString()} 존재하지않음");
            }
            BuffStatDic[entityStat] = (int)(data * SystemConst.Per);
            return BuffStatDic[entityStat] / SystemConst.Per;
        }

        /// <summary>
        /// Raw(만분률 이전값)값을 Set하는 함수 만분률값인지 반드시 확인바람
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public int SetBuffRawStat(EntityStat entityStat, int data)
        {
            if (!BuffStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} 캐릭터의 Buff {entityStat.ToString()} 존재하지않음");
            }
            BuffStatDic[entityStat] = data;
            return BuffStatDic[entityStat];
        }
        #endregion
    }
}