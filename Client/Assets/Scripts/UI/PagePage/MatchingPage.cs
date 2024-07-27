
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Client
{
    public class MatchingPage : UI_Scene
    {
        [SerializeField] private Button backBtn = null; // ��Ī ��� ��ư

        public override void Init()
        {
            base.Init();
            backBtn.onClick.AddListener(OnClickBackBtn);
        }

        // ��Ī ��� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickBackBtn()
        {
            Back();
        }
    }
}