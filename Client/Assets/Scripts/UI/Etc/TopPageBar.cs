// 2024/07/28 [이서연]
// 마법사 꾸미기 페이지 탭 슬롯

using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Client
{
    public class TopPageBar : UI_Base
    {
        [SerializeField] private Button   backBtn   = null; // 뒤로 가기 버튼
        [SerializeField] private Button   optionBtn = null; // 옵션 버튼
        [SerializeField] private Button   friendBtn = null; // 친구 관리 버튼

        private UI_Scene page = null; // 부모 페이지

        public void Init(UI_Scene page)
        {
            base.Init();
            this.page = page;
            backBtn.onClick.AddListener(OnClickBackBtn);
            optionBtn.onClick.AddListener(OnClickOptionBtn);
            friendBtn.onClick.AddListener(OnClickFriendBtn);
        }

        // 옵션 버튼을 눌렀을 때 호출됩니다.
        private void OnClickOptionBtn()
        {
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }

        // 친구 관리 버튼을 눌렀을 때 호출됩니다.
        private void OnClickFriendBtn()
        {
            UIManager.Instance.ShowPopupUI<FriendPopupPage>();

        }
        // 뒤로 가기 버튼을 눌렀을 때 호출됩니다.
        private void OnClickBackBtn()
        {
            page.Back();
        }
    }
}