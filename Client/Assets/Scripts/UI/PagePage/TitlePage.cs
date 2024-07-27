
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class TitlePage : UI_Scene
    {
        [SerializeField] private Button startBtn  = null; // ���� ��ư
        [SerializeField] private Button optionBtn = null; // �ɼ� ��ư
        [SerializeField] private Button exitBtn   = null; // ���� ��ư

        public override void Init()
        {
            base.Init();
            startBtn.onClick.AddListener(OnClickStartBtn);
            optionBtn.onClick.AddListener(OnClickOptionBtn);
            exitBtn.onClick.AddListener(OnClickExitBtn);
        }

        // ���� ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickStartBtn()
        {
            UIManager.Instance.ShowSceneUI<LobbyPage>();
        }
        // �ɼ� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickOptionBtn()
        {
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }
        // ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickExitBtn()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}