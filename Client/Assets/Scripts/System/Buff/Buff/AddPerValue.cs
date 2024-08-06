using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    // Value1 : EntityStat 을 만분율한 Per 만큼 더함 (합연산 곱연산 아직 확정 안남)
    public class AddPerValue : BuffBase
    {
        int deltaValue = 0;
        public AddPerValue(BuffData buffData) : base(buffData)
        {

        }

        public override void Dispel()
        {

            if (BuffTarget == null)
                return;

            if (BuffTarget.PlayerInfo == null)
                return;

            BuffTarget.PlayerInfo.BuffStatDic[BuffData.Stat] -= deltaValue;
            UpdateStat();
        }

        public override void Execute()
        {
            if (BuffTarget == null)
                return;

            if (BuffTarget.PlayerInfo == null)
                return;

            // 곱연산인지 합연산인지는 논의가 필요
            deltaValue = (int)(BuffTarget.PlayerInfo.NowStatDic[BuffData.Stat] * (BuffData.Value1 / SystemConst.Per));
            BuffTarget.PlayerInfo.BuffStatDic[BuffData.Stat] += deltaValue;
            UpdateStat();
        }

        public override void SetData(BuffData buffData)
        {
            BuffData = buffData;
        }
    }
}