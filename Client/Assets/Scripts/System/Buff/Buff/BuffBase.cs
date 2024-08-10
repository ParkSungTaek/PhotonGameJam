using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{

    public abstract class BuffBase
    {
        public BuffBase(BuffData buffData)
        {
            BuffData = buffData;
        }
        // ���� Ÿ��
        public virtual SystemEnum.BuffType BuffType => SystemEnum.BuffType.None;

        protected BuffData BuffData = null;

        // EntityInfo ������
        protected Player BuffUser = null;

        // EntityInfo �����
        protected Player BuffTarget = null;

        // ���������� Set
        public abstract void SetData(BuffData buffData);
        // ���� ��� ����
        public abstract void Execute();
        // ���� ����
        public abstract void Dispel();

        public virtual void SetBuffUser(PlayerRef buffUser)
        {
            if (EntityManager.Instance.SpawnedCharacters == null)
                return;
            if(EntityManager.Instance.SpawnedCharacters.ContainsKey(buffUser))
                BuffUser = EntityManager.Instance.SpawnedCharacters[buffUser];
        }
        public virtual void SetBuffTarget(PlayerRef buffTarget)
        {
            if (EntityManager.Instance.SpawnedCharacters == null)
                return;
            if (EntityManager.Instance.SpawnedCharacters.ContainsKey(buffTarget))
                BuffTarget = EntityManager.Instance.SpawnedCharacters[buffTarget];
        }

        protected void UpdateStat()
        {
            BuffTarget.PlayerInfo.NowStatDic[BuffData.Stat] = BuffTarget.PlayerInfo.EntityStatDic[BuffData.Stat] + BuffTarget.PlayerInfo.BuffStatDic[BuffData.Stat];
        }
    }
}
