// 2024/07/28 [이서연]
// 첫 타이틀 UI 페이지

using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class SelectSkillPage : UI_Popup
    {
        [SerializeField] private Button            selectBtn = null; // 선택 버튼
        [SerializeField] private SkillScrollSlot[] skillSlot = null; // 스킬 스크롤

        public override void Init()
        {
            base.Init();
            selectBtn.onClick.AddListener(OnClickSelectBtn);
        }

        // 마법 선택 버튼을 눌렀을 때 호출됩니다.
        private void OnClickSelectBtn()
        {
            Debug.Log("어떤 마법 선택함요");
            Back();
        }
      
    }
}