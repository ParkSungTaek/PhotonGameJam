
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class MatchingPage : UI_Scene
    {
        enum GameObjects
        {
        }
        enum Buttons
        {
            BackBtn,
        }
        enum Texts
        {
        }
        private Action<PointerEventData> ClickBackBtn = null; // 게임 시작 버튼 Action

        public override void Init()
        {
            base.Init();
            Bind<Button>(typeof(Buttons));
            ButtonBind();
        }

        #region Buttons
        private void ButtonBind()
        {
            ClickBackBtn = OnClickBackBtn;
            BindEvent(GetButton((int)Buttons.BackBtn).gameObject, ClickBackBtn);
        }
        // 게임 시작 버튼을 눌렀을 때 호출됩니다.
        private void OnClickBackBtn(PointerEventData even)
        {
            Back();
        }

        #endregion Buttons


    }
}