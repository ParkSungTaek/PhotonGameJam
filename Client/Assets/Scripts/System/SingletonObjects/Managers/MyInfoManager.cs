using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class FriendData
    {
        public long UID = 0; // 친구 UID
        public string name = "FriendName"; // 친구 닉네임
        public Dictionary<DecoType, DecoData> DecoInfo = new(); // 친구 꾸미기 정보
        public bool isOnline = false; // 온라인인가?
    }

    public class MyInfoManager : Singleton<MyInfoManager>
    {
        private string                         _nickName = "MyNickName"; // 닉네임
        private Dictionary<DecoType, DecoData> _decoInfo = new();        // 꾸미기 정보
        private Dictionary<long, FriendData> _friendList = new();        // 친구 정보

        private MyInfoManager()
        {

        }

        // 친구정보를 갱신합니다. TODO[이서연] : 친구기능 정해지면 내용 추가할것
        public void UpdateFriend()
        {

        }

        // 친구정보를 추가합니다. TODO[이서연] : 친구기능 정해지면 내용 추가할것
        public void AddFriend()
        {
         
        }

        // 친구정보를 삭제합니다. TODO[이서연] : 친구기능 정해지면 내용 추가할것
        public void DeleteFriend()
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

        // 친구목록을 반환합니다.
        public Dictionary<long, FriendData> GetFriends()
        {
            return _friendList;
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