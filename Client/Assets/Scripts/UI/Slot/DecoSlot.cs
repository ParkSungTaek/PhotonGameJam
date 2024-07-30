// 2024/07/28 [�̼���]
// ������ �ٹ̱� ������ �� ����

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using static Client.SystemEnum;

namespace Client
{
    public class DecoSlot : UI_Base
    {
        [SerializeField] private Image      icon     = null; // ������
        [SerializeField] private Button     mainBtn = null; // ���� ��ư
        [SerializeField] private TMP_Text   nameText = null; // �̸�
        [SerializeField] private GameObject selectGroup = null; // �̸�

        private DecoData    decoData     = null; // �ٹ̱� ������ Data
        private Action<DecoType, DecoData> selectAction = null; // ���õǾ��� �� Action

        public override void Init()
        {
            base.Init();
            mainBtn.onClick.AddListener(OnClickBtn);
        }

        // �ٹ̱� ������ �����͸� �����մϴ�.
        public void SetData(Action<DecoType, DecoData> action, DecoData data)
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

        // �ٹ̱� ������ ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickBtn()
        {
            if (selectAction == null) return;
            selectAction(decoData._type, decoData);
        }

        // � �������� ������ ���� ȣ��˴ϴ�.
        public void OnTabSelected(DecoType type, DecoData data)
        {
            selectGroup.SetActive(decoData == data && decoData._type == type);
        }
    }
}