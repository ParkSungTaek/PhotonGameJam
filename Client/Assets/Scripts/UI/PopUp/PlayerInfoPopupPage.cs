// 2024/07/28 [이서연]
// 옵션 UI 팝업 페이지

using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class PlayerInfoPopupPage : UI_Popup
    {
        [SerializeField] private PlayerInfoSlot playerInfos1 = null; // 친구 추가
        [SerializeField] private PlayerInfoSlot playerInfos2 = null; // 친구 추가
        [SerializeField] private Button optionBtn = null; // 옵션 버튼

        private List<PlayerInfo> players = null;
        public override void Init()
        {
            base.Init();
            optionBtn.onClick.AddListener(OnClickOptionBtn);
        }

        public override void ReOpenPopupUI()
        {

        }
        public void SetData(PlayerRef player, string name, int face, int body, int hair, int weapon, int hat, int cape)
        {
            if (player == null) return;
            if (player.PlayerId == 1)
            {
                playerInfos1.SetData(name, face, body, hair, weapon, hat, cape);
            }
            else
            {
                playerInfos1.SetData(name, face, body, hair, weapon, hat, cape);
            }
        }


        // 옵션 버튼을 눌렀을 때 호출됩니다.
        private void OnClickOptionBtn()
        {
            Back();
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }
    }
}