using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Client
{
    public class ChatUI : MonoBehaviour
    {
        [SerializeField] private Text           chatText   = null; // 입력란
        [SerializeField] private ScrollRect     scroll     = null; // 스크롤
        [SerializeField] private TMP_InputField inputField = null; // 입력란

        public void Awake()
        {
            inputField.onSubmit.AddListener(OnEndEdit);
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
            scroll.verticalNormalizedPosition = 0f;
        }
    }
}