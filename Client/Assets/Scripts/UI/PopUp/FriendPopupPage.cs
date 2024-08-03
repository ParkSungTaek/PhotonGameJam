// 2024/07/28 [�̼���]
// �ɼ� UI �˾� ������

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class FriendPopupPage : UI_Popup
    {
        [SerializeField] private Button         addFriendBtn = null; // ģ�� �߰�
        [SerializeField] private ScrollRect     scroll       = null; // ģ�� ��� ��ũ��
        [SerializeField] private FriendSlot     friendPrepeb   = null; // �ٹ̱� ���� ������
        [SerializeField] private TMP_InputField nameInput    = null; // ģ�� �̸� �Է¶�

        public override void Init()
        {
            base.Init();
            SetFriendDatas();
        }

        public override void ReOpenPopupUI()
        {
            SetFriendDatas();
        }

        // ģ�� ��ũ���� �����մϴ�. 
        public void SetFriendDatas()
        {
            Transform content = scroll.content;
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
            foreach (var data in MyInfoManager.Instance.GetFriends())
            {
                FriendData friend = data.Value;
                FriendSlot newSlot = Instantiate(friendPrepeb, content);
                newSlot.SetData(friend.name, friend.isOnline, friend.DecoInfo);
            }
            // (�ϴ� ģ�� �̱����̶� �ӽ� �θ� ����)
            FriendSlot anewSlot = Instantiate(friendPrepeb, content);
            anewSlot.SetData("�׽�Ʈ ģ�� 1", true, MyInfoManager.Instance.GetDecoData());

            FriendSlot bnewSlot = Instantiate(friendPrepeb, content);
            bnewSlot.SetData("�׽�Ʈ ģ�� 2", false, MyInfoManager.Instance.GetDecoData());
        }
    }
}