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
        // ���� Ÿ��
        public virtual SystemEnum.BuffType BuffType => SystemEnum.BuffType.None;
        //���� �̸�
        public virtual SystemEnum.Buffs Buff => SystemEnum.Buffs.None;

        // EntityInfo ������
        EntityInfo BuffUser = null;

        // EntityInfo �����
        EntityInfo BuffTarget = null;


        // ���������� Set
        public abstract void SetData(BuffData buffData); 
        // ���� ��� ����
        public abstract void Execute();
        // ���� ����
        public abstract void Dispel();

    }
}
