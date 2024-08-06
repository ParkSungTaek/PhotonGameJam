using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    // Value1 : EntityStat �� �������� Per ��ŭ ���� (�տ��� ������ ���� Ȯ�� �ȳ�)
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

            // ���������� �տ��������� ���ǰ� �ʿ�
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