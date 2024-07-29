// 2024/07/28 [�̼���]
// ��ų ������ UI

using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Client
{
    public class SkillScrollSlot : UI_Base
    {
        [SerializeField] private Image    skillIcon = null; // ��ų ������
        [SerializeField] private Button   rerollBtn = null; // ���� ��ư
        [SerializeField] private TMP_Text skillName = null; // ��ų �̸�
        [SerializeField] private TMP_Text skillDesc = null; // ��ų ����

        public override void Init()
        {
            base.Init();
            rerollBtn.onClick.AddListener(OnClickOptionBtn);
        }

        // ��ų �����͸� �����մϴ�.
        public void SetData()
        {

        }

        // ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickOptionBtn()
        {
            Debug.Log("���ѹ�ư ������");
        }
    }
}