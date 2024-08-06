using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Client
{
    public class ChatUI : MonoBehaviour
    {
        [SerializeField] private Text           chatText   = null; // �Է¶�
        [SerializeField] private ScrollRect     scroll     = null; // ��ũ��
        [SerializeField] private TMP_InputField inputField = null; // �Է¶�

        public void Awake()
        {
            inputField.onSubmit.AddListener(OnEndEdit);
        }

        // ä�� ������
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