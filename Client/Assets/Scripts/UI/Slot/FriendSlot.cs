// 2024/08/03 [�̼���]
// �÷��̾� ���� ( ģ�� ����Ʈ�� ���̽� ǥ�ÿ��� ��� ���� )

using UnityEngine;
using TMPro;
using static Client.SystemEnum;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Client
{
    public class FriendSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text     name         = null; // �г���
        [SerializeField] private GameObject   onlineGroup  = null; // �¶��� �׷�
        [SerializeField] private GameObject   offlineGroup = null; // �������� �׷�
        [SerializeField] private PlayerFaceUI playerUI     = null; // �÷��̾� ����

        public void SetData(string name, OnlineState onlineState, Dictionary<DecoType, DecoData> decoData)
        {
            this.name.SetText(name);
            SetOnline(onlineState);
            playerUI.SetPlayerDeco(decoData);
        }

        // ��/���������� �����մϴ�.
        public void SetOnline(OnlineState onlineState)
        {
            bool isOnline = onlineState != OnlineState.Offline;
            if (isOnline)
            {
                if(onlineState == OnlineState.Lobby)
                {
                    onlineGroup.GetComponentInChildren<TMP_Text>().text = "�κ�";
                }
                else if (onlineState == OnlineState.Game)
                {
                    onlineGroup.GetComponentInChildren<TMP_Text>().text = "������";
                }
            }

            onlineGroup.SetActive(isOnline);
            offlineGroup.SetActive(!isOnline);
        }
    }
}