// 2024/07/28 [�̼���]
// ���� �κ� UI ������

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class LobbyPage : UI_Scene
    {
        [SerializeField] private Button     inGameBtn       = null; // �ΰ��� �ٷΰ��� ��ư (�����ڿ�)
        [SerializeField] private Button     matchingBtn     = null; // ���� ��Ī ��ư
        [SerializeField] private Button     selectPlayerBtn = null; // ������ ���� ��ư
        [SerializeField] private Button     a = null; // ������ ���� ��ư
        [SerializeField] private Button     b = null; // ������ ���� ��ư
        [SerializeField] private TopPageBar topBar          = null; // ��ܹ�
        public override void Init()
        {
            base.Init();
            topBar.Init(this);
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
            UIManager.Instance.ShowSceneUI<DecoPage>();
        }

        // �ΰ��� �ٷΰ��� ��ư (�����ڿ�)�� ������ �� ȣ��˴ϴ�.
        private void OnClickInGameBtn()
        {
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
        }

    }
}