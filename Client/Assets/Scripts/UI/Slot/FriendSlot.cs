// 2024/08/03 [�̼���]
// �÷��̾� ���� ( ģ�� ����Ʈ�� ���̽� ǥ�ÿ��� ��� ���� )

using UnityEngine;
using TMPro;
using static Client.SystemEnum;
using System.Collections.Generic;

namespace Client
{
    public class FriendSlot : UI_Base
    {
        [SerializeField] private TMP_Text     name         = null; // �г���
        [SerializeField] private GameObject   onlineGroup  = null; // �¶��� �׷�
        [SerializeField] private GameObject   offlineGroup = null; // �������� �׷�
        [SerializeField] private PlayerFaceUI playerUI     = null; // �÷��̾� ����

        public void SetData(string name, bool isOnline, Dictionary<DecoType, DecoData> decoData)
        {
            this.name.SetText(name);
            SetOnline(isOnline);
            playerUI.SetPlayerDeco(decoData);
        }

        // ��/���������� �����մϴ�.
        public void SetOnline(bool isOnline)
        {
            onlineGroup.SetActive(isOnline);
            offlineGroup.SetActive(!isOnline);
        }
    }
}