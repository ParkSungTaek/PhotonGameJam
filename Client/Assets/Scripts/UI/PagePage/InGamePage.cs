// 2024/07/28 [�̼���]
// �ΰ��� UI ������

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class InGamePage : UI_Scene
    {
        [SerializeField] private Button optionBtn = null; // �ɼ� ��ư
        [SerializeField] private Button skillBtn = null; // ��ũ�� ���� ��ư (������)
        public override void Init()
        {
            base.Init();
            optionBtn.onClick.AddListener(OnClickOptionBtn);
            skillBtn.onClick.AddListener(OnClickSkillBtn);
        }

        // �ɼ� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickOptionBtn()
        {
            UIManager.Instance.ShowPopupUI<OptionPopupPage>();
        }

        // ��ũ�� ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickSkillBtn()
        {
            UIManager.Instance.ShowPopupUI<SelectSkillPage>();
        }
    }
}