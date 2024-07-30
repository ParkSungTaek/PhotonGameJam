// 2024/07/28 [이서연]
// 마법사 꾸미기 페이지 탭 슬롯

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using static Client.SystemEnum;

namespace Client
{
    public class DecoSlot : UI_Base
    {
        [SerializeField] private Image      icon     = null; // 아이콘
        [SerializeField] private Button     mainBtn = null; // 선택 버튼
        [SerializeField] private TMP_Text   nameText = null; // 이름
        [SerializeField] private GameObject selectGroup = null; // 이름

        private DecoData    decoData     = null; // 꾸미기 아이템 Data
        private Action<DecoType, int> selectAction = null; // 선택되었을 때 Action

        public override void Init()
        {
            base.Init();
            mainBtn.onClick.AddListener(OnClickBtn);
        }

        // 꾸미기 아이템 데이터를 세팅합니다.
        public void SetData(Action<DecoType, int> action, DecoData data)
        {
            selectAction = action;
            decoData = data;
            Texture2D texture = Resources.Load<Texture2D>($"Sprites/Characters/{decoData._type}/{decoData._resource}");
            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                icon.sprite = sprite;
            }
            nameText.SetText(decoData._name);
        }

        // 꾸미기 아이템 버튼을 눌렀을 때 호출됩니다.
        private void OnClickBtn()
        {
            if (selectAction == null) return;
            selectAction(decoData._type, decoData.index);
        }

        // 어떤 아이템이 눌렸을 때에 호출됩니다.
        public void OnTabSelected(DecoType type, int selectIndex)
        {
            selectGroup.SetActive(decoData.index == selectIndex && decoData._type == type);
        }
    }
}