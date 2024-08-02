using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class EntityInfo
    {
        public EntityBase MyEntity = null; //�ش� Entity�� ���� ����
        public Dictionary<EntityStat, int> EntityStatDic = new Dictionary<EntityStat, int>(); // Entity �⺻ ���ݿ� ���� �������� ���з� ���� Raw��
        public Dictionary<EntityStat, int> NowStatDic    = new Dictionary<EntityStat, int>();    // ���� ���ݿ� ���� �������� ���з� ���� Raw��
        public Dictionary<EntityStat, int> BuffStatDic   = new Dictionary<EntityStat, int>();   // ������ ���� ����ġ�� ���� �������� ���з� ���� Raw��

    }
}
