// 2024/07/30 [이서연]
// 마법사 꾸미기 페이지 탭 버튼

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Client.SystemEnum;
using System;

namespace Client
{
    public class DecoTabSlot : UI_Base
    {
        [SerializeField] private Image      tabIcon     = null;    // 꾸미기 탭 아이콘
        [SerializeField] private Button     mainBtn     = null;    // 버튼
        [SerializeField] private TMP_Text   tabName     = null;    // 꾸미기 탭 이름
        [SerializeField] private DecoType   type        = default; // 꾸미기 탭 종류
        [SerializeField] private GameObject selectGroup = null;    // 선택된 그룹

        private Action<DecoType> selectAction = null; // 선택되었을 때 Action

        public override void Init()
        {
            base.Init();
            mainBtn.onClick.AddListener(OnClickBtn);
        }

        // 꾸미기탭 데이터를 세팅합니다.
        public void SetData(Action<DecoType> action)
        {
            selectAction = action;
        }

        // 리롤 버튼을 눌렀을 때 호출됩니다.
        private void OnClickBtn()
        {
            if (selectAction == null) return;
            selectAction(type);
        }

        // 어떤 탭이 눌렸을 때에 호출됩니다.
        public void OnTabSelected(DecoType selectType)
        {
            selectGroup.SetActive(selectType == type);
        }
    }
}