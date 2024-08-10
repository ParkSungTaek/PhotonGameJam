// 2024/07/28 [�̼���]
// �ΰ��� UI ������

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
        [SerializeField] private Button optionBtn = null; // �ɼ� ��ư
        [SerializeField] private Button skillBtn = null; // ��ũ�� ���� ��ư (������)

        [SerializeField] private TMP_Text player1Name = null; // �÷��̾�1 �̸�
        [SerializeField] private TMP_Text player2Name = null; // �÷��̾�2 �̸�
        [SerializeField] private Image[] player1Hearts = null; // �÷��̾�1 ��Ʈ
        [SerializeField] private Image[] player2Hearts = null; // �÷��̾�2 ��Ʈ
        [SerializeField] private Sprite emptyHeart = null; // ���� ��Ʈ
        public override void Init()
        {
            base.Init();
            optionBtn.onClick.AddListener(OnClickOptionBtn);
            skillBtn.onClick.AddListener(OnClickSkillBtn);
        }

        // �ɼ� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickOptionBtn()
        {
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }

        // ��ũ�� ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickSkillBtn()
        {
            UIManager.Instance.ShowPopupUI<SelectSkillPage>();
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
    }
}