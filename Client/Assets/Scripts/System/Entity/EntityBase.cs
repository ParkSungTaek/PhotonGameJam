using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    /// <summary>
    /// ��ü�� ���� Base Ŭ���� 
    /// Awake �� �� �ݵ�� base.Awake����� ��
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
        /// ��ƼƼ �޴����� Set 
        /// </summary>
        private void Awake()
        {
            Type type = this.GetType();
            EntityManager.Instance.SetEntity(type ,this);
        }

        public long GetID() => _ID;
        public SystemEnum.EntityType GetEntityType() => _EntityType;

        // EntityManager ���� ���� �� ����
        public virtual void RemoveEntity()
        {
            Type type = this.GetType();
            EntityManager.Instance.RemoveEntity(type, this);
            Destroy(this.gameObject);
        }

    }
}

