using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class PlayerInfo : EntityInfo
    {
        private string                         _charName   = "Default Name"; // �г���
        private WeaponData                     _weaponData = null;           // Player ������
        private List<BuffBase>                 _buffBases  = null;           // �������� ����
        private EntityPlayerData               _playerData = null;           // Player ������
        private Dictionary<DecoType, DecoData> _decoData   = new();          // �ٹ̱� ������
        public List<MagicElement> MagicElements { get; set; } = new List<MagicElement>();

        #region BuffData

        // ������°� ���з� ���� ���ؼ� �ϴ� ���� �װ� �������� Get�ؼ� ��� �ѹ��� ��ȸ�ؼ� ��� 
        // enpum 

        #endregion

        public string                         CharName => _charName;
        public Dictionary<DecoType, DecoData> DecoData => _decoData;

        // ��������
        public bool IsLive { get; set; } = true;
        public PlayerInfo() 
        {
            
        }
        public PlayerInfo(int playerDataID = SystemConst.NoData, int weaponDataID = SystemConst.NoData, List<BuffBase> buffBases = null, Dictionary<DecoType, DecoData>decoInfo = null)
        {
            SetData(playerDataID, weaponDataID, buffBases, decoInfo);
        }
        // ���� ĳ���� ����, ���� ����, ���� ����Ʈ(���� Ȱ��ȭ) ���� Set
        public void SetData(int playerDataID = SystemConst.NoData, int weaponDataID = SystemConst.NoData, List<BuffBase> buffBases = null, Dictionary<DecoType, DecoData> decoInfo = null)
        {
            if (playerDataID != SystemConst.NoData)
            {
                _playerData = DataManager.Instance.GetData<EntityPlayerData>(playerDataID);
                if (_playerData == null)
                {
                    Debug.LogWarning($"PlayerInfo {playerDataID} �� �ش��ϴ� PlayerData ���� ã�� ����");
                    //return;
                    // ������ ������ ���̺��� ���� ���߿� �ݵ��!! ���� �ٶ�

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
                Debug.LogWarning($"SetDataWeaponData ���� ã�� ����");

            }
        }

        // �ٹ̱� �����͸� �����մϴ�.
        public void SetDecoData(DecoType type, DecoData decoData)
        {
            _decoData[type] = decoData;
        }

        // �г����� �����մϴ�.
        public void SetNickName(string name)
        {
            _charName = name;
        }

        // ���� ���� Ȱ��ȭ
        public void ExecuteBuff(BuffBase buff)
        {
            _buffBases.Add(buff);
            if (buff != null)
            {
                buff.Execute();
            }
        }
        // ���� ���� ��Ȱ��ȭ
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

        #region ������ Get ���� �ݵ�� ���з��� �������ּ���!!!!!
        /// <summary>
        /// ���з� ���� ���� Get�ϴ� �Լ�
        /// </summary>
        /// <returns></returns>
        public float GetStat(EntityStat entityStat)
        {
            if (!NowStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Now {entityStat.ToString()} ������������");
                return 0;
            }
            return NowStatDic[entityStat] / SystemConst.Per;
                   
        }

        /// <summary>
        /// ���з� �������� Get�ϴ� �Լ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <returns></returns>
        public int GetRawStat(EntityStat entityStat)
        {
            if (!NowStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Now {entityStat.ToString()} ������������");
            }
            return NowStatDic[entityStat];

        }

        /// <summary>
        /// ���з� ���� ���� Get�ϴ� �Լ�
        /// </summary>
        /// <returns></returns>
        public float GetEntityStat(EntityStat entityStat)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Entity {entityStat.ToString()} ������������");
                return 0;
            }
            return EntityStatDic[entityStat] / SystemConst.Per;

        }

        /// <summary>
        /// ���з� �������� Get�ϴ� �Լ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <returns></returns>
        public int GetEntityRawStat(EntityStat entityStat)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Entity {entityStat.ToString()} ������������");
            }
            return EntityStatDic[entityStat];

        }

        public float GetBuffStat(EntityStat entityStat)
        {
            if (!BuffStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Buff {entityStat.ToString()} ������������");
                return 0;
            }
            return BuffStatDic[entityStat] / SystemConst.Per;

        }

        /// <summary>
        /// ���з� �������� Get�ϴ� �Լ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <returns></returns>
        public int GetBuffRawStat(EntityStat entityStat)
        {
            if (!BuffStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Buff {entityStat.ToString()} ������������");
            }
            return BuffStatDic[entityStat];

        }

        /// <summary>
        /// �Ϲݰ� (���з� ���� ��) �� Set�ϴ� �Լ� ���з������� �ݵ�� Ȯ�ιٶ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public float SetStat(EntityStat entityStat, float data)
        {
            if (!NowStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Now {entityStat.ToString()} ������������");
            }
            NowStatDic[entityStat] = (int)(data * SystemConst.Per);
            return NowStatDic[entityStat] / SystemConst.Per;
        }

        /// <summary>
        /// Raw(���з� ������)���� Set�ϴ� �Լ� ���з������� �ݵ�� Ȯ�ιٶ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public int SetRawStat(EntityStat entityStat, int data)
        {
            if (!NowStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Now {entityStat.ToString()} ������������");
            }
            NowStatDic[entityStat] = data;
            return NowStatDic[entityStat];
        }

        /// <summary>
        /// �Ϲݰ� (���з� ���� ��) �� Set�ϴ� �Լ� ���з������� �ݵ�� Ȯ�ιٶ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public float SetEntityStat(EntityStat entityStat, float data)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Entity {entityStat.ToString()} ������������");
            }
            EntityStatDic[entityStat] = (int)(data * SystemConst.Per);
            return EntityStatDic[entityStat] / SystemConst.Per;
        }

        /// <summary>
        /// Raw(���з� ������)���� Set�ϴ� �Լ� ���з������� �ݵ�� Ȯ�ιٶ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public int SetEntityRawStat(EntityStat entityStat, int data)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Entity {entityStat.ToString()} ������������");
            }
            EntityStatDic[entityStat] = data;
            return EntityStatDic[entityStat];
        }

        /// <summary>
        /// �Ϲݰ� (���з� ���� ��) �� Set�ϴ� �Լ� ���з������� �ݵ�� Ȯ�ιٶ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public float SetBuffStat(EntityStat entityStat, float data)
        {
            if (!BuffStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Buff {entityStat.ToString()} ������������");
            }
            BuffStatDic[entityStat] = (int)(data * SystemConst.Per);
            return BuffStatDic[entityStat] / SystemConst.Per;
        }

        /// <summary>
        /// Raw(���з� ������)���� Set�ϴ� �Լ� ���з������� �ݵ�� Ȯ�ιٶ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public int SetBuffRawStat(EntityStat entityStat, int data)
        {
            if (!BuffStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ Buff {entityStat.ToString()} ������������");
            }
            BuffStatDic[entityStat] = data;
            return BuffStatDic[entityStat];
        }
        #endregion
    }
}