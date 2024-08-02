// 2024/07/28 [이서연]
// 옵션 UI 팝업 페이지

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Client.SystemEnum;

namespace Client
{
    public class CommonPopupPage : UI_Popup
    {
        [SerializeField] private Button   okBtn     = null; // 확인 버튼
        [SerializeField] private Button   cancelBtn = null; // 취소 버튼
        [SerializeField] private TMP_Text nameText  = null; // 제목
        [SerializeField] private TMP_Text descText  = null; // 내용

        private Action action = null;

        // 팝업 내용을 설정해줍니다.
        public void SetData(string name, string str, CommonPopuptype type, Action action)
        {
            cancelBtn.onClick.RemoveAllListeners();
            okBtn.onClick.RemoveAllListeners();

            cancelBtn.onClick.AddListener(OnClickCancelBtn);

            if (type == CommonPopuptype.OneBtn)
            {
                okBtn.onClick.AddListener(OnClickCancelBtn);
                cancelBtn.gameObject.SetActive(false);
            }
            else if (type == CommonPopuptype.TwoBtn)
            {
                okBtn.onClick.AddListener(OnClickOkBtn);
                cancelBtn.gameObject.SetActive(true);
            }

            nameText.SetText(name);
            descText.SetText(str);
            this.action = action;
        }

        // 확인 버튼을 눌렀을 때 호출됩니다.
        public void OnClickOkBtn()
        {
            if (action == null) return;
            action();
            Back();
        }

        // 닫기 버튼을 눌렀을 때 호출됩니다.
        public void OnClickCancelBtn()
        {
            Back();
        }
    }
}