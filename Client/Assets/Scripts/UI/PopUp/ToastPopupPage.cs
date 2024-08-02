// 2024/07/28 [�̼���]
// �ɼ� UI �˾� ������

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class ToastPopupPage : UI_Popup
    {
        [SerializeField] private TMP_Text descText = null; // ����

        // �佺Ʈ �˾� ������ �������ݴϴ�.
        public void SetData(string str, float time)
        {
            descText.SetText(str);
            Invoke("DestroyToastPopup", time);
        }

        // �佺Ʈ �˾��� �ݾ��ݴϴ�.
        public void DestroyToastPopup()
        {
            Back();
        }
    }
}