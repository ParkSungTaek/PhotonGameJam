using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// 객체의 기초 Base 클래스 
    /// Awake 쓸 때 반드시 base.Awake상속할 것
    /// </summary>
    public abstract class EntityBase : MonoBehaviour
    {
        protected long _ID;
        protected virtual SystemEnum.EntityType _EntityType => SystemEnum.EntityType.None;
        // Start is called before the first frame update
        protected EntityBase()
        {
            _ID = EntityManager.Instance.GetNextID();
        }

        /// <summary>
        /// 엔티티 메니저에 Set 
        /// </summary>
        private void Awake()
        {
            Type type = this.GetType();
            EntityManager.Instance.SetEntity(type ,this);
        }

        public long GetID() => _ID;
        public SystemEnum.EntityType GetEntityType() => _EntityType;

        // EntityManager 에서 제거 및 삭제
        public virtual void RemoveEntity()
        {
            Type type = this.GetType();
            EntityManager.Instance.RemoveEntity(type, this);
            Destroy(this.gameObject);
        }

    }
}

