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
        #region BuffData

        // ������°� ���з� ���� ���ؼ� �ϴ� ���� �װ� �������� Get�ؼ� ��� �ѹ��� ��ȸ�ؼ� ��� 
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
                    Debug.LogWarning($"PlayerInfo {weaponDataID} �� �ش��ϴ� WeaponData ���� ã�� ����");
                    //����� �ȳ��� ��������?
                    // ������ ������ ���̺��� ���� ���߿� �ݵ��!! ���� �ٶ�

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
                Debug.LogWarning($"SetDataWeaponData ���� ã�� ����");

            }
            else
            {
                EntityStatDic[EntityStat.AtkSpd] = _weaponData._atkSpd;
                EntityStatDic[EntityStat.NAtkSpd] = _weaponData._atkSpd;

                EntityStatDic[EntityStat.Att] = _weaponData._Att;
                EntityStatDic[EntityStat.NAtt] = _weaponData._Att;
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
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ {entityStat.ToString()} ������������");
                return 0;
            }
            return EntityStatDic[entityStat] / SystemConst.Per;
                   
        }

        /// <summary>
        /// ���з� �������� Get�ϴ� �Լ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <returns></returns>
        public int GetRawStat(EntityStat entityStat)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ {entityStat.ToString()} ������������");
            }
            return EntityStatDic[entityStat];

        }


        /// <summary>
        /// �Ϲݰ� (���з� ���� ��) �� Set�ϴ� �Լ� ���з������� �ݵ�� Ȯ�ιٶ�
        /// </summary>
        /// <param name="entityStat"></param>
        /// <param name="NonePer"></param>
        /// <returns></returns>
        public float SetStat(EntityStat entityStat, float data)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ {entityStat.ToString()} ������������");
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
        public int SetRawStat(EntityStat entityStat, int data)
        {
            if (!EntityStatDic.ContainsKey(entityStat))
            {
                Debug.LogWarning($"{_charName} ĳ������ {entityStat.ToString()} ������������");
            }
            EntityStatDic[entityStat] = data;
            return EntityStatDic[entityStat];
        }

        #endregion
    }
}