
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class LobbyPage : UI_Scene
    {
        [SerializeField] private Button inGameBtn       = null; // �ΰ��� �ٷΰ��� ��ư (�����ڿ�)
        [SerializeField] private Button matchingBtn     = null; // ���� ��Ī ��ư
        [SerializeField] private Button selectPlayerBtn = null; // ������ ���� ��ư

        public override void Init()
        {
            base.Init();
            matchingBtn.onClick.AddListener(OnClickMatchingBtn);
            selectPlayerBtn.onClick.AddListener(OnClickSelectPlayerBtn);
            inGameBtn.onClick.AddListener(OnClickInGameBtn);
        }


        // ���� ��Ī ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickMatchingBtn()
        {
            UIManager.Instance.ShowSceneUI<MatchingPage>();
        }

        // ������ ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickSelectPlayerBtn()
        {
        }

        // �ΰ��� �ٷΰ��� ��ư (�����ڿ�)�� ������ �� ȣ��˴ϴ�.
        private void OnClickInGameBtn()
        {
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
        }

    }
}