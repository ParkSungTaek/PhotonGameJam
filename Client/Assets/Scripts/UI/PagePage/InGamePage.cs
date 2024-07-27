
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class InGamePage : UI_Scene
    {
        [SerializeField] private Button optionBtn = null; // 옵션 버튼
        public override void Init()
        {
            base.Init();
            optionBtn.onClick.AddListener(OnClickOptionBtn);
        }

        // 옵션 버튼을 눌렀을 때 호출됩니다.
        private void OnClickOptionBtn()
        {
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }
    }
}