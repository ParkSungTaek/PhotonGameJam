
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
            SelectPlayerBtn,
        }
        enum Texts
        {
        }
        private Action<PointerEventData> ClickMatchingBtn     = null; // 랜덤 매칭 버튼 Action
        private Action<PointerEventData> ClickSelectPlayerBtn = null; // 마법사 설정 버튼 Action
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
            ClickSelectPlayerBtn = OnClickSelectPlayerBtn;
            BindEvent(GetButton((int)Buttons.GameStartBtn).gameObject, ClickMatchingBtn);
            BindEvent(GetButton((int)Buttons.SelectPlayerBtn).gameObject, ClickSelectPlayerBtn);
        }

        // 랜덤 매칭 버튼을 눌렀을 때 호출됩니다.
        private void OnClickMatchingBtn(PointerEventData even)
        {
            UIManager.Instance.ShowSceneUI<MatchingPage>();
        }

        // 마법사 설정 버튼을 눌렀을 때 호출됩니다.
        private void OnClickSelectPlayerBtn(PointerEventData even)
        {
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
        }

        #endregion Buttons


    }
}