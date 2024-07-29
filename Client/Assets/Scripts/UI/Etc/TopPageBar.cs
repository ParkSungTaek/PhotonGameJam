// 2024/07/28 [�̼���]
// ������ �ٹ̱� ������ �� ����

using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Client
{
    public class TopPageBar : UI_Base
    {
        [SerializeField] private Button   backBtn   = null; // �ڷ� ���� ��ư
        [SerializeField] private Button   optionBtn = null; // �ɼ� ��ư

        private UI_Scene page = null; // �θ� ������

        public void Init(UI_Scene page)
        {
            base.Init();
            this.page = page;
            backBtn.onClick.AddListener(OnClickBackBtn);
            optionBtn.onClick.AddListener(OnClickOptionBtn);
        }

        // �ɼ� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickOptionBtn()
        {
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }

        // �ڷ� ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickBackBtn()
        {
            page.Back();
        }
    }
}