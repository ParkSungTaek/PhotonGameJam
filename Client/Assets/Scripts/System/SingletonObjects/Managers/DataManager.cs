using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static Client.SystemEnum;


namespace Client
{
    /// <summary> 
    /// 데이터 매니저 (Sheet 데이터 관리)
    /// </summary>
    public class DataManager : Singleton<DataManager>
    {
        /// 로드한 적 있는 DataTable (Table 명을  Key1 데이터 ID를 Key2로 사용)
        Dictionary<string, Dictionary<int, SheetData>> _cache = new Dictionary<string, Dictionary<int, SheetData>>();

        private DataManager()
        {
            LoadData();
        }

        public T GetData<T>(int id) where T : SheetData
        {
            string key = typeof(T).Name;
            if (!_cache.ContainsKey(key))
            {
                Debug.LogError($"{key} 데이터 테이블은 존재하지 않습니다.");
                return null;
            }
            if (!_cache[key].ContainsKey(id))
            {
                Debug.LogError($"{key} 데이터에 ID {id}는 존재하지 않습니다.");
                return null;
            }
            T returnData = _cache[key][id] as T;
            if (returnData == null)
            {
                Debug.LogError($"{key} 데이터에 ID {id}는 존재하지만 {key}타입으로 변환 실패했습니다.");
                return null;

            }

            return returnData;
        }

        public void SetData<T>(int id, T data) where T : SheetData
        {
            string key = typeof(T).Name;
            if (_cache.ContainsKey(key))
            {
                Debug.LogWarning($"{key} 데이터 테이블은 이미 존재합니다.");
            }
            else
            {
                _cache.Add(key, new Dictionary<int, SheetData>());
            }

            if (_cache[key].ContainsKey(id))
            {
                Debug.LogWarning($"{key} 타입 ID: {id} 칼럼은 이미 존재합니다. !(주의) 게임 중 데이터 칼럼을 변경할 수 없습니다!");
            }
            else 
            {
                _cache[key].Add(id, data);
            }
        }

        public void LoadData()
        {
            Type baseType = typeof(SheetData);

            IEnumerable<Type> derivedTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(baseType));

            foreach (var type in derivedTypes)
            {
                SheetData instance = (SheetData)Activator.CreateInstance(type);


                if (_cache.ContainsKey(type.Name))
                {
                    Debug.LogWarning($"{type.Name} 데이터 테이블은 이미 존재합니다.");
                }
                else
                {
                    _cache.Add(type.Name, instance.LoadData());
                }
            }

            var t = GetData<TestData>(1);
            Debug.Log(t.name);

            var t2 = GetData<TestData2>(1);
            Debug.Log(t2.buff.ToString());

            var t4 = GetData<DecoData>(1);
            Debug.Log(t4._name.ToString());
        }
    }
}