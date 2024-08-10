// 2024/07/28 [�̼���]
// ù Ÿ��Ʋ UI ������

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class NickNamePage : UI_Scene
    {
        [SerializeField] private Button         checkBtn = null; // Ȯ�� ��ư
        [SerializeField] private TMP_InputField inputField = null; // �Է¶�

        private string nickName = string.Empty;

        public override void Init()
        {
            base.Init();
            checkBtn.onClick.AddListener(OnClickCheckBtn);
            inputField.onValueChanged.AddListener(OnClickCheckBtn);
        }

        // �̸� ���� ��ư�� ���� �� ȣ��˴ϴ�.
        private void OnClickCheckBtn()
        {
            MyInfoManager.Instance.SetNickName(nickName);
            UIManager.Instance.ShowCommonPopup("�̸� Ȯ��", nickName + "�� ���� ���� �̸��� �¾�?", SystemEnum.CommonPopuptype.TwoBtn, OnGameStart);
            // ä�� ���� ����
            var chatManager = ChatManager.Instance;
        }

        // �̸� ���� �Ϸ�� ȣ��˴ϴ�.
        private void OnGameStart()
        {
            UIManager.Instance.ShowSceneUI<LobbyPage>();
            MyInfoManager.Instance.SetTuto(true);
        }

        // �г����� ����� �� ȣ��˴ϴ�.
        private void OnClickCheckBtn( string nickName )
        {
            this.nickName = nickName;
        }
    }
}