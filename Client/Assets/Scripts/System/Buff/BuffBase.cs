using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{
    public struct BuffData
    {
        public int Value1;
        public int Value2;
        public int Value3;

        public BuffData(int value1, int value2, int value3)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }
        

    }
    public abstract class BuffBase : MonoBehaviour
    {
        // 버프 타입
        public virtual SystemEnum.BuffType BuffType => SystemEnum.BuffType.None;
        //버프 이름
        public virtual SystemEnum.Buffs Buff => SystemEnum.Buffs.None;

        // EntityInfo 시전자
        EntityInfo BuffUser = null;

        // EntityInfo 대상자
        EntityInfo BuffTarget = null;


        // 버프데이터 Set
        public abstract void SetData(BuffData buffData); 
        // 버프 기능 실행
        public abstract void Execute();
        // 버프 헤제
        public abstract void Dispel();

    }
}
