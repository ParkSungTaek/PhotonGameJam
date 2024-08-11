// 2024/08/03 [�̼���]
// �÷��̾� ���� ( ģ�� ����Ʈ�� ���̽� ǥ�ÿ��� ��� ���� )

using UnityEngine;
using TMPro;
using static Client.SystemEnum;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Client
{
    public class PlayerInfoSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text          name         = null; // �г���
        [SerializeField] private PlayerFaceUI      playerUI     = null; // �÷��̾� ����
        [SerializeField] private SkillScrollSlot[] skills       = null; // ��ų��

        // �÷��̾� �����͸� �����մϴ�.
        public void SetData(PlayerInfo player)
        {
            this.name.SetText(player.CharName);
            playerUI.SetPlayerDeco(player.DecoData);
            foreach( var slot in skills )
            {
                slot.gameObject.SetActive(false);
            }

            if( player.MagicLists.Count > 0)
            {
                for (int i = 0; i < player.MagicLists.Count; ++i)
                {
                    skills[i].gameObject.SetActive(true);
                    skills[i].SetData(player.MagicLists[i], null, null);
                }
            }

        }
    }
}