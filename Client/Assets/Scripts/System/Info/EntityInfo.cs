using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class EntityInfo
    {
        public EntityBase MyEntity = null; //해당 Entity에 대한 정보
        public Dictionary<EntityStat, int> EntityStatDic = new Dictionary<EntityStat, int>(); // Entity 기본 스텟에 대한 스텟정보 만분률 이전 Raw값
        public Dictionary<EntityStat, int> NowStatDic    = new Dictionary<EntityStat, int>();    // 현재 스텟에 대한 스텟정보 만분률 이전 Raw값
        public Dictionary<EntityStat, int> BuffStatDic   = new Dictionary<EntityStat, int>();   // 버프에 의한 증감치에 대한 스텟정보 만분률 이전 Raw값

    }
}
