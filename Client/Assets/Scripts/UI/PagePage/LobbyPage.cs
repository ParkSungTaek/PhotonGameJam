
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
        private Action<PointerEventData> ClickMatchingBtn = null; // ���� ��Ī ��ư Action
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

        // ���� ��Ī ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickMatchingBtn(PointerEventData even)
        {
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
        }

        #endregion Buttons


    }
}