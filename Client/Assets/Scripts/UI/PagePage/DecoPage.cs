// 2024/07/28 [�̼���]
// ù Ÿ��Ʋ UI ������

using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class DecoPage : UI_Scene
    {
        [SerializeField] private Button      backBtn    = null; // �ڷ� ���� ��ư
        [SerializeField] private Button      saveBtn    = null; // ���� �Ա� ��ư
        [SerializeField] private DecoSlot    decoPrepeb = null; // �ٹ̱� ���� ������
        [SerializeField] private DecoTabSlot tabPrepeb  = null; // �ٹ̱� ���� �� ������
        [SerializeField] private ScrollRect  scroll     = null; // �ٹ̱� ���� ��ũ��

        public override void Init()
        {
            base.Init();
            backBtn.onClick.AddListener(Back);
            saveBtn.onClick.AddListener(OnClickSaveBtn);
        }

        // ���� �Ա� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickSaveBtn()
        {
        }
    }
}