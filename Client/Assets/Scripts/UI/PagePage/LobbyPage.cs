
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class LobbyPage : UI_Scene
    {
        [SerializeField] private Button inGameBtn       = null; // 인게임 바로가기 버튼 (개발자용)
        [SerializeField] private Button matchingBtn     = null; // 랜덤 매칭 버튼
        [SerializeField] private Button selectPlayerBtn = null; // 마법사 설정 버튼

        public override void Init()
        {
            base.Init();
            matchingBtn.onClick.AddListener(OnClickMatchingBtn);
            selectPlayerBtn.onClick.AddListener(OnClickSelectPlayerBtn);
            inGameBtn.onClick.AddListener(OnClickInGameBtn);
        }


        // 랜덤 매칭 버튼을 눌렀을 때 호출됩니다.
        private void OnClickMatchingBtn()
        {
            UIManager.Instance.ShowSceneUI<MatchingPage>();
        }

        // 마법사 설정 버튼을 눌렀을 때 호출됩니다.
        private void OnClickSelectPlayerBtn()
        {
        }

        // 인게임 바로가기 버튼 (개발자용)을 눌렀을 때 호출됩니다.
        private void OnClickInGameBtn()
        {
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
        }

    }
}