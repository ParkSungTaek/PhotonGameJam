using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class PlayerInfo : EntityInfo
    {
        private PlayerData _playerData    = null; // Player ������
        private WeaponData _weaponData    = null; // Player ������
        private List<BuffBase> _buffBases = null; // �������� ����
        public string CharName { get; set; } = "DefaultName";
        #region BuffData

        // ������°� ���з� ���� ���ؼ� �ϴ� ���� �װ� �������� Get�ؼ� ��� �ѹ��� ��ȸ�ؼ� ��� 
        // enpum 
        
        #endregion



        public PlayerInfo() 
        {
            
        }
        public PlayerInfo(int playerDataID = SystemConst.NoData, int weaponDataID = SystemConst.NoData, List<BuffBase> buffBases = null)
        {
            SetData(playerDataID, weaponDataID, buffBases);
        }
        // ���� ĳ���� ����, ���� ����, ���� ����Ʈ(���� Ȱ��ȭ) ���� Set
        public void SetData(int playerDataID = SystemConst.NoData, int weaponDataID = SystemConst.NoData, List<BuffBase> buffBases = null)
        {
            if (playerDataID != SystemConst.NoData)
            {
                _playerData = DataManager.Instance.GetData<PlayerData>(playerDataID);
                if (_playerData == null)
                {
                    Debug.LogWarning($"PlayerInfo {playerDataID} �� �ش��ϴ� PlayerData ���� ã�� ����");
                    //return;
                    // ������ ������ ���̺��� ���� ���߿� �ݵ��!! ���� �ٶ�

                    _playerData = new PlayerData();
                }

                EntityStatDic[EntityStat.JumpP] = _playerData.JumpPower;
                EntityStatDic[EntityStat.NJumpP] = _playerData.JumpPower;

                EntityStatDic[EntityStat.HP] = _playerData.HP;
                EntityStatDic[EntityStat.NHP] = _playerData.HP;

                EntityStatDic[EntityStat.MovSpd] = _playerData.Speed;
                EntityStatDic[EntityStat.NMovSpd] = _playerData.Speed;

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
                    EntityStatDic[EntityStat.AtkSpd] = _weaponData.AtkSpd;
                    EntityStatDic[EntityStat.NAtkSpd] = _weaponData.AtkSpd;
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
                Debug.LogWarning($"{CharName} ĳ������ {entityStat.ToString()} ������������");
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
                Debug.LogWarning($"{CharName} ĳ������ {entityStat.ToString()} ������������");
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
                Debug.LogWarning($"{CharName} ĳ������ {entityStat.ToString()} ������������");
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
                Debug.LogWarning($"{CharName} ĳ������ {entityStat.ToString()} ������������");
            }
            EntityStatDic[entityStat] = data;
            return EntityStatDic[entityStat];
        }

        #endregion
    }
}