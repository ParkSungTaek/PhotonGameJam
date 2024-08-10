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

        [SerializeField] private TMP_Text player1 = null; // �÷��̾�1 ���ھ�
        [SerializeField] private TMP_Text player2 = null; // �÷��̾�2 ���ھ�
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