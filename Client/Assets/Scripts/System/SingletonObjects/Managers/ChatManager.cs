using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using ExitGames.Client.Photon;
using Fusion.Photon.Realtime;
using Client;
using static Client.SystemEnum;
using UnityEngine.SocialPlatforms;
using System;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace Client
{
    public class ChatManager : Singleton<ChatManager>, IChatClientListener
    {
        private ChatClient chatClient;
        private string userName;
        private string name => MyInfoManager.Instance.GetNickName();


        public string currentChannelName;

        public InputField inputField;
        public Text outputText;

        public string totalText { get; set; }

        private ChatManager()
        {
            Application.runInBackground = true;

            userName = System.Environment.UserName;
            currentChannelName = "Channel 001";

            chatClient = new ChatClient(this);
            chatClient.Connect(PhotonAppSettings.Global.AppSettings.AppIdChat, "1.0", new Photon.Chat.AuthenticationValues(name));

            AddLine(string.Format("연결시도", userName));

            GameManager.Instance.AddOnUpdate(UpdateChat);

            SetState(SystemEnum.OnlineState.Lobby);
        }

        public void TestPushMessage(string msg)
        {
            chatClient.PublishMessage(currentChannelName, msg);
        }

        public void AddLine(string lineString)
        {
            totalText += lineString + "\r\n";
        }

        // 전체 채팅
        public void SendPublicChat(string channel, string text)
        {
            if (chatClient.State == ChatState.ConnectedToFrontEnd)
            {
                chatClient.PublishMessage(channel, text);
            }
        }

        // 귓속말
        public void SendPrivateChat(string target, string text)
        {
            if (chatClient.State == ChatState.ConnectedToFrontEnd)
            {
                chatClient.SendPrivateMessage(target, text);
            }
        }

        // 채널 구독
        public void Subscibe(params string[] channels)
        {
            chatClient.Subscribe(channels, 10);
        }

        // 채널 구독 해제
        public void UnSubscibe(params string[] channels)
        {
            chatClient.Unsubscribe(channels);
        }

        // 본인 현재 상태 설정
        public void SetState(OnlineState state)
        {
            chatClient.SetOnlineStatus((int)state, "changeChange");
        }

        // 친구 추가
        public void AddFriends(params string[] friends)
        {
            chatClient.AddFriends(friends);
        }

        // 채팅 업데이트
        void UpdateChat()
        {
            chatClient.Service();
        }

        ///  Chat Handler =============================================
        // 어플 종료
        public void OnApplicationQuit()
        {
            if (chatClient != null)
            {
                SetState(OnlineState.Offline);
                chatClient.Disconnect();
            }
        }

        public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
        {
            if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
            {
                Debug.LogError(message);
            }
            else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
            {
                Debug.LogWarning(message);
            }
            else
            {
                Debug.Log(message);
            }
        }

        public void OnConnected()
        {
            AddLine("서버에 연결되었습니다.");

            chatClient.Subscribe(new string[] { currentChannelName }, 10);
        }

        public void OnDisconnected()
        {
            AddLine("서버에 연결이 끊어졌습니다.");
            SetState(OnlineState.Offline);
        }

        public void OnChatStateChange(ChatState state)
        {
            Debug.Log("OnChatStateChange = " + state);
        }

        public void OnSubscribed(string[] channels, bool[] results)
        {
            AddLine(string.Format("채널 입장 ({0})", string.Join(",", channels)));
        }

        public void OnUnsubscribed(string[] channels)
        {
            AddLine(string.Format("채널 퇴장 ({0})", string.Join(",", channels)));
        }

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            for (int i = 0; i < messages.Length; i++)
            {
                AddLine(string.Format("{0} : {1}", senders[i], messages[i].ToString()));
            }
        }

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            Debug.Log("OnPrivateMessage : " + message);
        }

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            foreach (var data in MyInfoManager.Instance.GetFriends())
            {
                if (data.Value.name == user) 
                {
                    data.Value.onlineState = (OnlineState)status;
                    break;
                }
            }

            Debug.Log("status : " + string.Format("{0} is {1}, Msg : {2} ", user, status, message));
        }

        public void OnUserSubscribed(string channel, string user)
        {
            throw new System.NotImplementedException();
        }

        public void OnUserUnsubscribed(string channel, string user)
        {
            throw new System.NotImplementedException();
        }
    }
}