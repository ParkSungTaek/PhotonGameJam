// 2024/07/28 [�̼���]
// ������ �ٹ̱� ������ �� ����

using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Client
{
    public class TopPageBar : MonoBehaviour
    {
        [SerializeField] private Button   backBtn    = null; // �ڷ� ���� ��ư
        [SerializeField] private Button   optionBtn  = null; // �ɼ� ��ư
        [SerializeField] private Button   friendBtn  = null; // ģ�� ���� ��ư
        [SerializeField] private TMP_Text playerName = null; // ���� �̸�

        private UI_Scene page = null; // �θ� ������

        public void Init(UI_Scene page)
        {
            this.page = page;
            playerName.SetText(MyInfoManager.Instance.GetNickName());
            backBtn.onClick.AddListener(OnClickBackBtn);
            optionBtn.onClick.AddListener(OnClickOptionBtn);
            friendBtn.onClick.AddListener(OnClickFriendBtn);
        }

        // �ɼ� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickOptionBtn()
        {
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }

        // ģ�� ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickFriendBtn()
        {
            UIManager.Instance.ShowPopupUI<FriendPopupPage>();

        }
        // �ڷ� ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickBackBtn()
        {
            page.Back();
        }
    }
}