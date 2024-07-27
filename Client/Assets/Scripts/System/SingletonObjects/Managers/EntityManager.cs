using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    /// <summary>
    /// Entity 컨트롤용 매니저
    /// Entity 게임 내 컨트롤 가능 객체의 최소단위
    /// </summary>
    public class EntityManager : Singleton<EntityManager>
    {
        // 존재하는 Entity (Entity Type 명을 Key1 Entity ID를 Key2로 사용)
        Dictionary<string, Dictionary<long, EntityBase>> _cache = new Dictionary<string, Dictionary<long, EntityBase>>();
        // 고유 ID 생성 
        long _nextID = 0;
        #region 생성자
        EntityManager() { }
        #endregion

        // 고유 ID 생성
        public long GetNextID() => _nextID++;

       public T GetEntity<T>(long ID) where T : EntityBase
       {
            string key = typeof(T).ToString();
            if (!_cache.ContainsKey(typeof(T).ToString()))
            {
                Debug.LogWarning($"{typeof(T).ToString()} 타입을 찾을 수 없음");
                return null;
            }
            if (_cache[key].ContainsKey(ID))
            {
                Debug.LogWarning($"{key} 타입의 ID: {ID}을 찾을 수 없음");
                return null;
            }
            T findEntity = _cache[key][ID] as T;
            if (findEntity == null)
            {
                Debug.LogWarning($"{key} 타입의 ID: {ID}을 {key} 타입으로 변환 불가");
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
                Debug.LogWarning($"{key} 타입의 ID: {data.GetID()}가 이미 존재함");
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
                Debug.LogWarning($"{key} 타입의 ID: {data.GetID()}가 이미 존재함");
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
                Debug.LogWarning($"삭제 실패: {key} 타입이 EntityManager에 의해 관리되지 않음");
                return false;
            }
            if (_cache[key].ContainsKey(data.GetID()))
            {
                _cache[key].Remove(data.GetID());
                return true;
            }
            else
            {
                Debug.LogWarning($"삭제 실패: {key} 타입의 ID: {data.GetID()}가 이미 존재하지않음");
                return false;
            }
        }
        public bool RemoveEntity<T>(T data) where T : EntityBase
        {
            string key = typeof(T).ToString();
            if (!_cache.ContainsKey(key))
            {
                Debug.LogWarning($"삭제 실패: {key} 타입이 EntityManager에 의해 관리되지 않음");
                return false;
            }
            if (_cache[key].ContainsKey(data.GetID()))
            {
                _cache[key].Remove(data.GetID());
                return true;
            }
            else
            {
                Debug.LogWarning($"삭제 실패: {key} 타입의 ID: {data.GetID()}가 이미 존재하지않음");
                return false;
            }
        }
    }
}