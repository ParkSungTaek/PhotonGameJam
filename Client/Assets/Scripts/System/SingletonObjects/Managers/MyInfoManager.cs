using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class FriendData
    {
        public long UID = 0; // 친구 UID
        public string name = "FriendName"; // 친구 닉네임
        public Dictionary<DecoType, DecoData> DecoInfo = new(); // 친구 꾸미기 정보
        public OnlineState onlineState = OnlineState.Offline; // 온라인 상태
    }

    public class MatchData
    {
        public bool isMatch = false; // 매칭 여부
        public int playerCount = 2; // 최대 명수
    }

    public class MyInfoManager : Singleton<MyInfoManager>
    {
        private string                         _nickName = "MyNickName"; // 닉네임
        private Dictionary<DecoType, DecoData> _decoInfo = new();        // 꾸미기 정보
        private Dictionary<long, FriendData> _friendList = new();        // 친구 정보
        private MatchData _matchData = new MatchData();
        private bool _isTuto = false;

        // 임시로 넣음(친구 인덱스)
        private int friendIndex = 0;

        private MyInfoManager()
        {

        }

        // 친구정보를 갱신합니다. TODO[이서연] : 친구기능 정해지면 내용 추가할것
        public void UpdateFriend()
        {

        }

        // 친구정보를 추가합니다. TODO[이서연] : 친구기능 정해지면 내용 추가할것
        public void AddFriend(string _name)
        {
            var friend = new FriendData()
            {
                UID = friendIndex,
                name = _name,
                DecoInfo = MyInfoManager.Instance.GetDecoData(),
                onlineState = OnlineState.Offline,
            };

            _friendList[friendIndex++] = friend;
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
            for( int i =0; i<(int)DecoType.MaxCount; ++i)
            {
                if (_decoInfo.ContainsKey((DecoType)i) == false)
                    _decoInfo.Add((DecoType)i, DataManager.Instance.GetAllData<DecoData>().Find(x => x._type == (DecoType)i));
            }
            return _decoInfo;
        }

        public void SetMatchData(bool match, int count)
        {
            _matchData.isMatch = match;
            _matchData.playerCount = count;
        }

        public MatchData GetMatchData()
        {
            return _matchData;
        }

        public void SetTuto(bool tuto)
        {
            _isTuto = tuto;
        }

        public bool GetTuto()
        {
            return _isTuto;
        }
    }
}