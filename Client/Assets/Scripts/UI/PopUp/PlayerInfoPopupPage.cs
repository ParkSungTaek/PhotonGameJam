// 2024/07/28 [�̼���]
// �ɼ� UI �˾� ������

using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class PlayerInfoPopupPage : UI_Popup
    {
        [SerializeField] private PlayerInfoSlot playerInfos1 = null; // ģ�� �߰�
        [SerializeField] private PlayerInfoSlot playerInfos2 = null; // ģ�� �߰�
        [SerializeField] private Button optionBtn = null; // �ɼ� ��ư

        private List<PlayerInfo> players = null;
        public override void Init()
        {
            base.Init();
            optionBtn.onClick.AddListener(OnClickOptionBtn);
        }

        public override void ReOpenPopupUI()
        {

        }
        public void SetData(PlayerRef player, string name, int face, int body, int hair, int weapon, int hat, int cape, int magic1, int magic2, int magic3, int magic4)
        {
            if (player == null) return;
            if (player.PlayerId == 1)
            {
                playerInfos1.SetData(name, face, body, hair, weapon, hat, cape, magic1, magic2, magic3, magic4);
            }
            else
            {
                playerInfos1.SetData(name, face, body, hair, weapon, hat, cape, magic1, magic2, magic3, magic4);
            }
        }


        // �ɼ� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickOptionBtn()
        {
            Back();
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }
    }
}