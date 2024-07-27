
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class MatchingPage : UI_Scene
    {
        [SerializeField] private Button backBtn = null; // 매칭 취소 버튼

        public override void Init()
        {
            base.Init();
            backBtn.onClick.AddListener(OnClickBackBtn);
        }

        // 매칭 취소 버튼을 눌렀을 때 호출됩니다.
        private void OnClickBackBtn()
        {
            Back();
        }
    }
}