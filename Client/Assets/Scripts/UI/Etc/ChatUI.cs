using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class ChatUI : MonoBehaviour
    {
        [SerializeField] private Text           chatText   = null; // 입력란
        [SerializeField] private ScrollRect     scroll     = null; // 스크롤
        [SerializeField] private TMP_InputField inputField = null; // 입력란
        [SerializeField] private Button onBtn = null; // 입력란
        [SerializeField] private GameObject group = null; // 입력란
        
        private bool isChatOn = false;


        public void Awake()
        {
            inputField.onSubmit.AddListener(OnEndEdit);
            onBtn.onClick.AddListener(OnClick);
        }

        // 채팅 보내기
        private void OnEndEdit(string text)
        {
            ChatManager.Instance.SendPublicChat(ChatManager.Instance.currentChannelName, text);
            inputField.text = "";
        }

        // 채팅 보내기
        private void OnClick()
        {
            isChatOn = !isChatOn;
            group.SetActive(isChatOn);
        }

        private void Update()
        {
            chatText.text = ChatManager.Instance.totalText;
            scroll.verticalNormalizedPosition = 0f;
        }
    }
}