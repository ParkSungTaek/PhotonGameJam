
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class TitlePage : UI_Scene
    {
        [SerializeField] private Button startBtn  = null; // 시작 버튼
        [SerializeField] private Button optionBtn = null; // 옵션 버튼
        [SerializeField] private Button exitBtn   = null; // 종료 버튼

        public override void Init()
        {
            base.Init();
            startBtn.onClick.AddListener(OnClickStartBtn);
            optionBtn.onClick.AddListener(OnClickOptionBtn);
            exitBtn.onClick.AddListener(OnClickExitBtn);
        }

        // 게임 시작 버튼을 눌렀을 때 호출됩니다.
        private void OnClickStartBtn()
        {
            UIManager.Instance.ShowSceneUI<LobbyPage>();
        }
        // 옵션 버튼을 눌렀을 때 호출됩니다.
        private void OnClickOptionBtn()
        {
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }
        // 종료 버튼을 눌렀을 때 호출됩니다.
        private void OnClickExitBtn()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}