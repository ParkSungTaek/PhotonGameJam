// 2024/07/30 [�̼���]
// ������ �ٹ̱� ������ �� ��ư

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Client.SystemEnum;
using System;

namespace Client
{
    public class DecoTabSlot : UI_Base
    {
        [SerializeField] private Image      tabIcon     = null;    // �ٹ̱� �� ������
        [SerializeField] private Button     mainBtn     = null;    // ��ư
        [SerializeField] private TMP_Text   tabName     = null;    // �ٹ̱� �� �̸�
        [SerializeField] private DecoType   type        = default; // �ٹ̱� �� ����
        [SerializeField] private GameObject selectGroup = null;    // ���õ� �׷�

        private Action<DecoType> selectAction = null; // ���õǾ��� �� Action

        public override void Init()
        {
            base.Init();
            mainBtn.onClick.AddListener(OnClickBtn);
        }

        // �ٹ̱��� �����͸� �����մϴ�.
        public void SetData(Action<DecoType> action)
        {
            selectAction = action;
        }

        // ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickBtn()
        {
            if (selectAction == null) return;
            selectAction(type);
        }

        // � ���� ������ ���� ȣ��˴ϴ�.
        public void OnTabSelected(DecoType selectType)
        {
            selectGroup.SetActive(selectType == type);
        }
    }
}