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
            nameInput.onSubmit.AddListener(OnEndEdit);
            addFriendBtn.onClick.AddListener(OnClickButton);
        }

        private void OnEndEdit(string text)
        {
            MyInfoManager.Instance.AddFriend(text);
            ChatManager.Instance.AddFriends(text);
            nameInput.text = "";

            SetFriendDatas();
        }

        private void OnClickButton()
        {
            MyInfoManager.Instance.AddFriend(nameInput.text);
            ChatManager.Instance.AddFriends(nameInput.text);
            nameInput.text = "";

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
                newSlot.SetData(friend.name, friend.onlineState, friend.DecoInfo);
            }
        }
    }
}