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
        [SerializeField] private Button skillBtn = null; // 스크롤 선택 버튼 (개발자)

        [SerializeField] private TMP_Text player1 = null; // 플레이어1 스코어
        [SerializeField] private TMP_Text player2 = null; // 플레이어2 스코어
        public override void Init()
        {
            base.Init();
            optionBtn.onClick.AddListener(OnClickOptionBtn);
            skillBtn.onClick.AddListener(OnClickSkillBtn);
        }

        // 옵션 버튼을 눌렀을 때 호출됩니다.
        private void OnClickOptionBtn()
        {
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }

        // 스크롤 선택 버튼을 눌렀을 때 호출됩니다.
        private void OnClickSkillBtn()
        {
            UIManager.Instance.ShowPopupUI<SelectSkillPage>();
        }

        public int GetPlayerScore(PlayerRef player)
        {
            if( player.PlayerId == 1)
            {
                return int.Parse(player2.text);
            }
            else
            {
                return int.Parse(player1.text);
            }
        }

        public void SetPlayerScore(PlayerRef player, int score)
        {
            if (player.PlayerId == 1)
            {
                player2.text = score.ToString();
            }
            else
            {
                player1.text = score.ToString();
            }
        }
    }
}