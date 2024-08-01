using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    /// <summary>
    /// UI매니저 
    /// </summary>
    public class MyInfoManager : Singleton<MyInfoManager>
    {
        private Dictionary<DecoType, DecoData> _decoInfo = new(); // 꾸미기 정보

        private MyInfoManager()
        {

        }

        // 꾸미기 데이터를 세팅합니다.
        public void SetDecoData(DecoType type, DecoData decoData)
        {
            if (_decoInfo.ContainsKey(type) == false)
            {
                _decoInfo.Add(type, new DecoData());
            }
            _decoInfo[type] = decoData;
        }

        public Dictionary<DecoType, DecoData> GetDecoData()
        {
            return _decoInfo;
        }
    }
}