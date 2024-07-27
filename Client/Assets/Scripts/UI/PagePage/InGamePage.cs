
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class InGamePage : UI_Scene
    {
        enum GameObjects
        {
        }
        enum Buttons
        {
            OptionBtn
        }
        enum Texts
        {
        }
        private Action<PointerEventData> ClickOptionBtn = null; // 옵션 버튼 Action
        public override void Init()
        {
            base.Init();
            Bind<Button>(typeof(Buttons));
            ButtonBind();
        }

        #region Buttons
        private void ButtonBind()
        {
            ClickOptionBtn = OnClickOptionBtn;
            BindEvent(GetButton((int)Buttons.OptionBtn).gameObject, ClickOptionBtn);
        }

        // 옵션 버튼을 눌렀을 때 호출됩니다.
        private void OnClickOptionBtn(PointerEventData even)
        {
            UIManager.Instance.ShowPopupUI<OptionPopup>();
        }

        #endregion Buttons


    }
}