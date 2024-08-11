using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Client
{
    public abstract class DotBuff : BuffBase
    {

        [Networked] private TickTimer CoolTime { get; set; } // °ø°Ý ÄðÅ¸ÀÓ

        public Action Active { get; set; }
        public bool IsDoAction { get; set; } = true;

        public DotBuff(BuffData buffData) : base(buffData)
        {

        }

        public override void Dispel()
        {
            IsDoAction = false;
            if (BuffTarget.PlayerInfo.DotAction.Contains(Active))
            {
                BuffTarget.PlayerInfo.DotAction.Remove(Active);
            }
            
        }

        public override void Execute()
        {
            BuffTarget.PlayerInfo.DotAction.Add(DotTimer);
        }

        public override void SetData(BuffData buffData)
        {
            BuffData = buffData;
        }

        public void DotTimer()
        {
            if (!IsDoAction)
                return;
            
            if (CoolTime.ExpiredOrNotRunning(BuffTarget.Runner))
            {
                CoolTime = TickTimer.CreateFromSeconds(BuffTarget.Runner, BuffData.Value1 / SystemConst.Per);
                DoAction();
            }
        }

        public abstract void DoAction();
    }
}