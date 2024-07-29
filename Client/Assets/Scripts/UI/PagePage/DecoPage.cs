// 2024/07/28 [이서연]
// 첫 타이틀 UI 페이지

using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class DecoPage : UI_Scene
    {
        [SerializeField] private Button      backBtn    = null; // 뒤로 가기 버튼
        [SerializeField] private Button      saveBtn    = null; // 갈아 입기 버튼
        [SerializeField] private DecoSlot    decoPrepeb = null; // 꾸미기 슬롯 프리펩
        [SerializeField] private ScrollRect  scroll     = null; // 꾸미기 슬롯 스크롤
        [SerializeField] private TopPageBar  topBar     = null; // 상단바
        [SerializeField] private DecoTabSlot tabPrepeb  = null; // 꾸미기 슬롯 탭 프리펩

        public override void Init()
        {
            base.Init();
            backBtn.onClick.AddListener(Back);
            saveBtn.onClick.AddListener(OnClickSaveBtn);
            topBar.Init(this);
        }

        // 갈아 입기 버튼을 눌렀을 때 호출됩니다.
        private void OnClickSaveBtn()
        {
        }
    }
}