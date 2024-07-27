
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class LobbyPage : UI_Scene
    {
        enum GameObjects
        {
        }
        enum Buttons
        {
            GameStartBtn,
        }
        enum Texts
        {
        }
        private Action<PointerEventData> ClickMatchingBtn = null; // ·£´ý ¸ÅÄª ¹öÆ° Action
        public override void Init()
        {
            base.Init();
            Bind<Button>(typeof(Buttons));
            ButtonBind();
        }

        #region Buttons
        private void ButtonBind()
        {
            ClickMatchingBtn = OnClickMatchingBtn;
            BindEvent(GetButton((int)Buttons.GameStartBtn).gameObject, ClickMatchingBtn);
        }

        // ·£´ý ¸ÅÄª ¹öÆ°À» ´­·¶À» ¶§ È£ÃâµË´Ï´Ù.
        private void OnClickMatchingBtn(PointerEventData even)
        {
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
        }

        #endregion Buttons


    }
}