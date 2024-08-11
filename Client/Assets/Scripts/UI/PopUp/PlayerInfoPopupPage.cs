// 2024/07/28 [이서연]
// 옵션 UI 팝업 페이지

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class PlayerInfoPopupPage : UI_Popup
    {
        [SerializeField] private PlayerInfoSlot[] playerInfos = null; // 친구 추가
        [SerializeField] private Button optionBtn = null; // 옵션 버튼

        private List<PlayerInfo> players = null;
        public override void Init()
        {
            base.Init();
            optionBtn.onClick.AddListener(OnClickOptionBtn);
            players = new();

            foreach( var player in EntityManager.Instance.SpawnedCharacters )
            {
                players.Add(player.Value.PlayerInfo);
            }

            SetData();
        }

        public override void ReOpenPopupUI()
        {
            players = new();

            foreach (var player in EntityManager.Instance.SpawnedCharacters)
            {
                players.Add(player.Value.PlayerInfo);
            }

            SetData();
        }
        public void SetData()
        {
            foreach (var slot in playerInfos)
            {
                slot.gameObject.SetActive(false);
            }
            for (int i=0; i< players.Count; ++i )
            {
                playerInfos[i].gameObject.SetActive(true);
                playerInfos[i].SetData(players[i]);
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