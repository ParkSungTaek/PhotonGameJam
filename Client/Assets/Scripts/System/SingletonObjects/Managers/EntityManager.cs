using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    /// <summary>
    /// Entity ��Ʈ�ѿ� �Ŵ���
    /// Entity ���� �� ��Ʈ�� ���� ��ü�� �ּҴ���
    /// </summary>
    public class EntityManager : Singleton<EntityManager>
    {
        // �����ϴ� Entity (Entity Type ���� Key1 Entity ID�� Key2�� ���)
        Dictionary<string, Dictionary<long, EntityBase>> _cache = new Dictionary<string, Dictionary<long, EntityBase>>();
        // ���� ID ���� 
        long _nextID = 0;
        #region ������
        EntityManager() { }
        #endregion

        // ���� ID ����
        public long GetNextID() => _nextID++;

       public T GetEntity<T>(long ID) where T : EntityBase
       {
            string key = typeof(T).ToString();
            if (!_cache.ContainsKey(typeof(T).ToString()))
            {
                Debug.LogWarning($"{typeof(T).ToString()} Ÿ���� ã�� �� ����");
                return null;
            }
            if (_cache[key].ContainsKey(ID))
            {
                Debug.LogWarning($"{key} Ÿ���� ID: {ID}�� ã�� �� ����");
                return null;
            }
            T findEntity = _cache[key][ID] as T;
            if (findEntity == null)
            {
                Debug.LogWarning($"{key} Ÿ���� ID: {ID}�� {key} Ÿ������ ��ȯ �Ұ�");
                return null;
            }
            return findEntity;
       }

        public bool SetEntity<T>(T data) where T : EntityBase
        {
            string key = typeof(T).ToString();
            if (!_cache.ContainsKey(key))
            {
                _cache.Add(key, new Dictionary<long, EntityBase>());
            }
            if (_cache[key].ContainsKey(data.GetID()))
            {
                Debug.LogWarning($"{key} Ÿ���� ID: {data.GetID()}�� �̹� ������");
                return false;
            }
            _cache[key].Add(data.GetID(),data);
            return true;
        }

        public bool SetEntity(Type type, EntityBase data)
        {
            string key = type.ToString();
            if (!_cache.ContainsKey(key))
            {
                _cache.Add(key, new Dictionary<long, EntityBase>());
            }
            if (_cache[key].ContainsKey(data.GetID()))
            {
                Debug.LogWarning($"{key} Ÿ���� ID: {data.GetID()}�� �̹� ������");
                return false;
            }
            _cache[key].Add(data.GetID(), data);
            return true;
        }

        public bool RemoveEntity(Type type, EntityBase data)
        {
            string key = type.ToString();
            if (!_cache.ContainsKey(key))
            {
                Debug.LogWarning($"���� ����: {key} Ÿ���� EntityManager�� ���� �������� ����");
                return false;
            }
            if (_cache[key].ContainsKey(data.GetID()))
            {
                _cache[key].Remove(data.GetID());
                return true;
            }
            else
            {
                Debug.LogWarning($"���� ����: {key} Ÿ���� ID: {data.GetID()}�� �̹� ������������");
                return false;
            }
        }
        public bool RemoveEntity<T>(T data) where T : EntityBase
        {
            string key = typeof(T).ToString();
            if (!_cache.ContainsKey(key))
            {
                Debug.LogWarning($"���� ����: {key} Ÿ���� EntityManager�� ���� �������� ����");
                return false;
            }
            if (_cache[key].ContainsKey(data.GetID()))
            {
                _cache[key].Remove(data.GetID());
                return true;
            }
            else
            {
                Debug.LogWarning($"���� ����: {key} Ÿ���� ID: {data.GetID()}�� �̹� ������������");
                return false;
            }
        }
    }
}