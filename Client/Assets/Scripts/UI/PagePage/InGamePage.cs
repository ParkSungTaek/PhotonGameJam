// 2024/07/28 [이서연]
// 인게임 UI 페이지

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using Fusion;
using Unity.VisualScripting;

namespace Client
{
    public class InGamePage : UI_Scene
    {
        [SerializeField] private Button optionBtn = null; // 옵션 버튼

        [SerializeField] private TMP_Text player1Name = null; // 플레이어1 이름
        [SerializeField] private TMP_Text player2Name = null; // 플레이어2 이름
        [SerializeField] private Image[] player1Hearts = null; // 플레이어1 하트
        [SerializeField] private Image[] player2Hearts = null; // 플레이어2 하트
        [SerializeField] private Sprite emptyHeart = null; // 까진 하트
        public override void Init()
        {
            base.Init();
            optionBtn.onClick.AddListener(OnClickOptionBtn);
        }

        // 옵션 버튼을 눌렀을 때 호출됩니다.
        private void OnClickOptionBtn()
        {
            EntityManager.Instance.MyPlayer._PlayerInfoPopupPage = UIManager.Instance.ShowPopupUI<PlayerInfoPopupPage>();
        }

        public int GetPlayerScore(PlayerRef player)
        {
            if( player.PlayerId == 1)
            {
                return int.Parse(player1Name.text);
            }
            else
            {
                return int.Parse(player2Name.text);
            }
        }

        public void SetPlayerDeathScore(PlayerRef player, int score)
        {
            if (player.PlayerId == 1)
            {
                player1Hearts[score].sprite = emptyHeart;
            }
            else
            {
                player2Hearts[score].sprite = emptyHeart;
            }
        }

        public void SetPlayerName(PlayerRef player, string name)
        {
            if (player.PlayerId == 1)
            {
                player1Name.text = name;
            }
            else
            {
                player2Name.text = name;
            }
        }

        public string GetPlayerName(PlayerRef player)
        {
            if (player.PlayerId == 1)
            {
                return player1Name.text;
            }
            else
            {
                return player2Name.text;
            }
        }

        public string GetRevusPlayerName(PlayerRef player)
        {
            if (player.PlayerId == 1)
            {
                return player2Name.text;
            }
            else
            {
                return player1Name.text;
            }
        }
    }
}