// 2024/07/28 [�̼���]
// ���� ��Ī �ε� UI ������

using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class MatchingPage : UI_Scene
    {
        [SerializeField] private Button backBtn = null; // ��Ī ��� ��ư

        public override void Init()
        {
            base.Init();
            backBtn.onClick.AddListener(OnClickBackBtn);
        }

        // ��Ī ��� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickBackBtn()
        {
            Back();
            NetworkManager.Instance.NetworkHandler.RetrunLobby();
        }
    }
}