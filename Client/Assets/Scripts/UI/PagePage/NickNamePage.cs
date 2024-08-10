// 2024/07/28 [이서연]
// 첫 타이틀 UI 페이지

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class NickNamePage : UI_Scene
    {
        [SerializeField] private Button         checkBtn = null; // 확인 버튼
        [SerializeField] private TMP_InputField inputField = null; // 입력란

        private string nickName = string.Empty;

        public override void Init()
        {
            base.Init();
            checkBtn.onClick.AddListener(OnClickCheckBtn);
            inputField.onValueChanged.AddListener(OnClickCheckBtn);
        }

        // 이름 설정 버튼을 누를 때 호출됩니다.
        private void OnClickCheckBtn()
        {
            MyInfoManager.Instance.SetNickName(nickName);
            UIManager.Instance.ShowCommonPopup("이름 확인", nickName + "이 정말 너의 이름이 맞아?", SystemEnum.CommonPopuptype.TwoBtn, OnGameStart);
            // 채팅 서버 접속
            var chatManager = ChatManager.Instance;
        }

        // 이름 설정 완료시 호출됩니다.
        private void OnGameStart()
        {
            UIManager.Instance.ShowSceneUI<LobbyPage>();
            MyInfoManager.Instance.SetTuto(true);
        }

        // 닉네임이 변경될 때 호출됩니다.
        private void OnClickCheckBtn( string nickName )
        {
            this.nickName = nickName;
        }
    }
}