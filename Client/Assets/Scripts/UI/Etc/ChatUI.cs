using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class ChatUI : MonoBehaviour
    {
        [SerializeField] private Text           chatText   = null; // �Է¶�
        [SerializeField] private ScrollRect     scroll     = null; // ��ũ��
        [SerializeField] private TMP_InputField inputField = null; // �Է¶�
        [SerializeField] private Button onBtn = null; // �Է¶�
        [SerializeField] private GameObject group = null; // �Է¶�
        
        private bool isChatOn = false;


        public void Awake()
        {
            inputField.onSubmit.AddListener(OnEndEdit);
            onBtn.onClick.AddListener(OnClick);
        }

        // ä�� ������
        private void OnEndEdit(string text)
        {
            ChatManager.Instance.SendPublicChat(ChatManager.Instance.currentChannelName, text);
            inputField.text = "";
        }

        // ä�� ������
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