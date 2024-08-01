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
        private string                         _nickName = "MyNickName"; // 닉네임
        private Dictionary<DecoType, DecoData> _decoInfo = new(); // 꾸미기 정보

        private MyInfoManager()
        {

        }

        // 닉네임을 세팅합니다.
        public void SetNickName(string name)
        {
            _nickName = name;
        }

        // 닉네임을 반환합니다.
        public string GetNickName()
        {
            return _nickName;
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

        // 꾸미기 데이터를 반환합니다.
        public Dictionary<DecoType, DecoData> GetDecoData()
        {
            return _decoInfo;
        }
    }
}