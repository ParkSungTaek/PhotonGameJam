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

        private string desc = string.Empty;
        private float time = 0.0f;

        // �佺Ʈ �˾� ������ �������ݴϴ�.
        public void SetData(string str, float time)
        {
            this.time = time;
            this.desc = str;
            descText.SetText(desc);
            Invoke("DestroyToastPopup", time);
        }

        // �佺Ʈ �˾��� �ݾ��ݴϴ�.
        public void DestroyToastPopup()
        {
            Back();
        }
    }
}