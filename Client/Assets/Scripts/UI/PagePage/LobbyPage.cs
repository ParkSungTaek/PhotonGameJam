// 2024/07/28 [이서연]
// 메인 로비 UI 페이지

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Fusion;

namespace Client
{
    public class LobbyPage : UI_Scene
    {
        [SerializeField] private Button     inGameBtn       = null; // 인게임 바로가기 버튼 (개발자용)
        [SerializeField] private Button     matchingBtn     = null; // 랜덤 매칭 버튼
        [SerializeField] private Button     selectPlayerBtn = null; // 마법사 설정 버튼
        [SerializeField] private Button     a = null; // 마법사 설정 버튼
        [SerializeField] private Button     b = null; // 마법사 설정 버튼
        [SerializeField] private TopPageBar topBar          = null; // 상단바

        [SerializeField] private Button     hostBtn         = null; // 호스트 입장 버튼(개발자용)
        public override void Init()
        {
            base.Init();
            topBar.Init(this);
            matchingBtn.onClick.AddListener(OnClickMatchingBtn);
            selectPlayerBtn.onClick.AddListener(OnClickSelectPlayerBtn);
            inGameBtn.onClick.AddListener(OnClickInGameBtn);

            hostBtn.onClick.AddListener(OnClickHostBtn);
        }

        // 랜덤 매칭 버튼을 눌렀을 때 호출됩니다.
        private void OnClickMatchingBtn()
        {
            UIManager.Instance.ShowSceneUI<MatchingPage>();
        }

        // 마법사 설정 버튼을 눌렀을 때 호출됩니다.
        private void OnClickSelectPlayerBtn()
        {
            UIManager.Instance.ShowSceneUI<DecoPage>();
        }

        // 인게임 바로가기 버튼 (개발자용)을 눌렀을 때 호출됩니다.
        private void OnClickInGameBtn()
        {
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
        }

        private void OnClickHostBtn()
        {
            NetworkManager.Instance.mode = GameMode.AutoHostOrClient;
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
        }

    }
}