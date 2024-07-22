using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Client
{
    /// <summary> 
    /// ������ �Ŵ��� (Sheet ������ ����)
    /// </summary>
    public class DataManager : Singleton<DataManager>
    {
        /// �ε��� �� �ִ� DataTable (Table ����  Key1 ������ ID�� Key2�� ���)
        Dictionary<string, Dictionary<int, SheetData>> _cache = new Dictionary<string, Dictionary<int, SheetData>>();

        public T GetData<T>(int id) where T : SheetData
        {
            string key = typeof(T).ToString();
            if (_cache.ContainsKey(key))
            {
                Debug.LogError($"{key} ������ ���̺��� �������� �ʽ��ϴ�.");
                return null;
            }
            if (_cache[key].ContainsKey(id))
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
            string key = typeof(T).ToString();
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


    }
}