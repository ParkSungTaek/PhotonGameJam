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
    /// ������ �Ŵ��� (Sheet ������ ����)
    /// </summary>
    public class DataManager : Singleton<DataManager>
    {
        /// �ε��� �� �ִ� DataTable (Table ����  Key1 ������ ID�� Key2�� ���)
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
                Debug.LogError($"{key} ������ ���̺��� �������� �ʽ��ϴ�.");
                return null;
            }
            if (!_cache[key].ContainsKey(id))
            {
                Debug.LogError($"{key} �����Ϳ� ID {id}�� �������� �ʽ��ϴ�.");
                return null;
            }
            T returnData = _cache[key][id] as T;
            if (returnData == null)
            {
                Debug.LogError($"{key} �����Ϳ� ID {id}�� ���������� {key}Ÿ������ ��ȯ �����߽��ϴ�.");
                return null;

            }

            return returnData;
        }

        public void SetData<T>(int id, T data) where T : SheetData
        {
            string key = typeof(T).Name;
            if (_cache.ContainsKey(key))
            {
                Debug.LogWarning($"{key} ������ ���̺��� �̹� �����մϴ�.");
            }
            else
            {
                _cache.Add(key, new Dictionary<int, SheetData>());
            }

            if (_cache[key].ContainsKey(id))
            {
                Debug.LogWarning($"{key} Ÿ�� ID: {id} Į���� �̹� �����մϴ�. !(����) ���� �� ������ Į���� ������ �� �����ϴ�!");
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
                    Debug.LogWarning($"{type.Name} ������ ���̺��� �̹� �����մϴ�.");
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