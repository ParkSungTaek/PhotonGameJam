using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class EntityInfo
    {
        public EntityBase MyEntity = null; //�ش� Entity�� ���� ����
        public Dictionary<EntityStat, int> EntityStatDic = new Dictionary<EntityStat, int>(); //�ش� Entity�� ���� �������� ���з� ���� Raw��
    }
}
