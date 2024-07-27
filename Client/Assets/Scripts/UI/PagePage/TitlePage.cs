
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
        private Action<PointerEventData> ClickStartBtn = null;
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
        private void OnClickStartBtn(PointerEventData even)
        {
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.Loading, true);
        }

        #endregion Buttons


    }
}