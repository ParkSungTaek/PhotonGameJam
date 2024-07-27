
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class TitlePage : UI_Scene
    {
        enum GameObjects
        {
        }
        enum Buttons
        {
            StartBtn,
        }
        enum Texts
        {
        }
        private Action<PointerEventData> ClickStartBtn = null; // 게임 시작 버튼 Action

        public override void Init()
        {
            base.Init();
            Bind<Button>(typeof(Buttons));
            ButtonBind();
        }

        #region Buttons
        private void ButtonBind()
        {
            ClickStartBtn = OnClickStartBtn;
            BindEvent(GetButton((int)Buttons.StartBtn).gameObject, ClickStartBtn);
        }
        // 게임 시작 버튼을 눌렀을 때 호출됩니다.
        private void OnClickStartBtn(PointerEventData even)
        {
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
        }

        #endregion Buttons


    }
}