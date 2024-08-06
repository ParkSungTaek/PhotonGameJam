using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    // Value1 : EntityStat 을 만분율한만큼 더함 
    public class AddFixedValue : BuffBase
    {
        public AddFixedValue(BuffData buffData) : base(buffData)
        {

        }

        public override void Dispel()
        {

            if (BuffTarget == null)
                return;

            if (BuffTarget.PlayerInfo == null)
                return;

            BuffTarget.PlayerInfo.BuffStatDic[BuffData.Stat] -= BuffData.Value1;
            BuffTarget.PlayerInfo.NowStatDic[BuffData.Stat] = BuffTarget.PlayerInfo.EntityStatDic[BuffData.Stat] + BuffTarget.PlayerInfo.BuffStatDic[BuffData.Stat];
        }

        public override void Execute()
        {
            if (BuffTarget == null)
                return;

            if (BuffTarget.PlayerInfo == null)
                return;

            BuffTarget.PlayerInfo.BuffStatDic[BuffData.Stat] += BuffData.Value1;
            BuffTarget.PlayerInfo.NowStatDic[BuffData.Stat] = BuffTarget.PlayerInfo.EntityStatDic[BuffData.Stat] + BuffTarget.PlayerInfo.BuffStatDic[BuffData.Stat];
        }

        public override void SetData(BuffData buffData)
        {
            BuffData = buffData;
        }
    }
}