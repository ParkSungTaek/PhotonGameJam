// 2024/07/28 [�̼���]
// ù Ÿ��Ʋ UI ������

using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class SelectSkillPage : UI_Popup
    {
        [SerializeField] private Button            selectBtn = null; // ���� ��ư
        [SerializeField] private SkillScrollSlot[] skillSlot = null; // ��ų ��ũ��

        public override void Init()
        {
            base.Init();
            selectBtn.onClick.AddListener(OnClickSelectBtn);
        }

        // ���� ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickSelectBtn()
        {
            Debug.Log("� ���� �����Կ�");
            Back();
        }
      
    }
}