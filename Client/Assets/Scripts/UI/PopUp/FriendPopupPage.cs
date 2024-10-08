// 2024/07/28 [이서연]
// 옵션 UI 팝업 페이지

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class FriendPopupPage : UI_Popup
    {
        [SerializeField] private Button         addFriendBtn = null; // 친구 추가
        [SerializeField] private ScrollRect     scroll       = null; // 친구 목록 스크롤
        [SerializeField] private FriendSlot     friendPrepeb   = null; // 꾸미기 슬롯 프리펩
        [SerializeField] private TMP_InputField nameInput    = null; // 친구 이름 입력란

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

        // 친구 스크롤을 세팅합니다. 
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