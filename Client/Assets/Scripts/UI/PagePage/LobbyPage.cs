
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
        private Action<PointerEventData> ClickMatchingBtn     = null; // ���� ��Ī ��ư Action
        private Action<PointerEventData> ClickSelectPlayerBtn = null; // ������ ���� ��ư Action
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

        // ���� ��Ī ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickMatchingBtn(PointerEventData even)
        {
            UIManager.Instance.ShowSceneUI<MatchingPage>();
        }

        // ������ ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickSelectPlayerBtn(PointerEventData even)
        {
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
        }

        #endregion Buttons


    }
}