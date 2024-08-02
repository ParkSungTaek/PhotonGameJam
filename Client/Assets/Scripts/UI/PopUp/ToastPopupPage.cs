// 2024/07/28 [이서연]
// 옵션 UI 팝업 페이지

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class ToastPopupPage : UI_Popup
    {
        [SerializeField] private TMP_Text descText = null; // 내용

        // 토스트 팝업 내용을 설정해줍니다.
        public void SetData(string str, float time)
        {
            descText.SetText(str);
            Invoke("DestroyToastPopup", time);
        }

        // 토스트 팝업을 닫아줍니다.
        public void DestroyToastPopup()
        {
            Back();
        }
    }
}