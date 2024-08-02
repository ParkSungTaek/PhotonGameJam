// 2024/07/28 [�̼���]
// �ɼ� UI �˾� ������

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
        [SerializeField] private Button   okBtn     = null; // Ȯ�� ��ư
        [SerializeField] private Button   cancelBtn = null; // ��� ��ư
        [SerializeField] private TMP_Text nameText  = null; // ����
        [SerializeField] private TMP_Text descText  = null; // ����

        private Action action = null;

        // �˾� ������ �������ݴϴ�.
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

        // Ȯ�� ��ư�� ������ �� ȣ��˴ϴ�.
        public void OnClickOkBtn()
        {
            if (action == null) return;
            action();
            Back();
        }

        // �ݱ� ��ư�� ������ �� ȣ��˴ϴ�.
        public void OnClickCancelBtn()
        {
            Back();
        }
    }
}