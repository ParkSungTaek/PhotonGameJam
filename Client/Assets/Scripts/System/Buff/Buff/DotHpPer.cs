using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    // Value1 : 시간
    // Value2 : HP 증감 %
    public class DotHpPer : DotBuff
    {
        public DotHpPer(BuffData buffData) : base(buffData)
        {
        }

        public override void DoAction()
        {
            BuffTarget.OnDamage(BuffData.Value2/SystemConst.Per);
        }
    }
}