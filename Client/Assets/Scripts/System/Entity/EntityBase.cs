using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public abstract class EntityBase : MonoBehaviour
    {
        protected long _ID;
        protected virtual SystemEnum.EntityType _EntityType => SystemEnum.EntityType.None;
        // Start is called before the first frame update
        protected EntityBase()
        {
            _ID = EntityManager.Instance.GetNextID();
        }

        public long GetID() => _ID;
        public SystemEnum.EntityType GetEntityType() => _EntityType;

    }
}

