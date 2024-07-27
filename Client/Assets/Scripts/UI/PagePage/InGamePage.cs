
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
        private Action<PointerEventData> ClickOptionBtn = null; // �ɼ� ��ư Action
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

        // �ɼ� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickOptionBtn(PointerEventData even)
        {
            UIManager.Instance.ShowPopupUI<OptionPopup>();
        }

        #endregion Buttons


    }
}