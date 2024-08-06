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
        // 버프 타입
        public virtual SystemEnum.BuffType BuffType => SystemEnum.BuffType.None;

        protected BuffData BuffData = null;

        // EntityInfo 시전자
        protected Player BuffUser = null;

        // EntityInfo 대상자
        protected Player BuffTarget = null;

        // 버프데이터 Set
        public abstract void SetData(BuffData buffData);
        // 버프 기능 실행
        public abstract void Execute();
        // 버프 헤제
        public abstract void Dispel();

        public virtual void SetBuffUser(Player buffUser)
        {
            BuffUser = buffUser;
        }
        public virtual void SetBuffTarget(Player buffTarget)
        {
            BuffTarget = buffTarget;
        }

        protected void UpdateStat()
        {
            BuffTarget.PlayerInfo.NowStatDic[BuffData.Stat] = BuffTarget.PlayerInfo.EntityStatDic[BuffData.Stat] + BuffTarget.PlayerInfo.BuffStatDic[BuffData.Stat];
        }
    }
}
