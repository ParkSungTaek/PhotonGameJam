using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class EntityInfo
    {
        public EntityBase MyEntity = null; //해당 Entity에 대한 정보
        public Dictionary<EntityStat, int> EntityStatDic = new Dictionary<EntityStat, int>(); //해당 Entity에 대한 스텟정보 만분률 이전 Raw값
    }
}
