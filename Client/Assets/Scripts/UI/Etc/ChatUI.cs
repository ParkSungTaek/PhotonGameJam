using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Client
{
    public class ChatUI : UI_Base
    {
        [SerializeField] private Text           chatText   = null; // 입력란
        [SerializeField] private TMP_InputField inputField = null; // 입력란

        public override void Init()
        {
            base.Init();
            inputField.onEndEdit.AddListener(OnEndEdit);
        }

        // 채팅 보내기
        private void OnEndEdit(string text)
        {
            ChatManager.Instance.SendPublicChat(ChatManager.Instance.currentChannelName, text);
            inputField.text = "";
        }

        private void Update()
        {
            chatText.text = ChatManager.Instance.totalText;
        }
    }
}