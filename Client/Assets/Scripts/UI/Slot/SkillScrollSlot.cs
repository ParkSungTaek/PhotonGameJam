// 2024/07/28 [이서연]
// 스킬 양피지 UI

using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Client
{
    public class SkillScrollSlot : UI_Base
    {
        [SerializeField] private Image    skillIcon = null; // 스킬 아이콘
        [SerializeField] private Button   rerollBtn = null; // 리롤 버튼
        [SerializeField] private TMP_Text skillName = null; // 스킬 이름
        [SerializeField] private TMP_Text skillDesc = null; // 스킬 설명

        public override void Init()
        {
            base.Init();
            rerollBtn.onClick.AddListener(OnClickOptionBtn);
        }

        // 스킬 데이터를 세팅합니다.
        public void SetData()
        {

        }

        // 리롤 버튼을 눌렀을 때 호출됩니다.
        private void OnClickOptionBtn()
        {
            Debug.Log("리롤버튼 누름요");
        }
    }
}