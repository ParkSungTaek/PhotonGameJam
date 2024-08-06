// 2024/07/28 [�̼���]
// ��ų ������ UI

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Client
{
    public class SkillScrollSlot : MonoBehaviour
    {
        [SerializeField] private Image    bookImg   = null; // å �̹���
        [SerializeField] private Image    skillIcon = null; // ��ų ������
        [SerializeField] private Button   rerollBtn = null; // ���� ��ư
        [SerializeField] private Button   selectBtn = null; // ���� ��ư

        [SerializeField] private TMP_Text skillName = null; // ��ų �̸�
        [SerializeField] private TMP_Text skillDesc = null; // ��ų ����
        private Action<SkillScrollSlot> rerollAction = null;
        private MagicBookData magicBookData = null; // ������ 
        public void Awake()
        {
            rerollBtn.onClick.AddListener(OnClickReRollBtn);
            selectBtn.onClick.AddListener(OnClickSelectBtn);
        }

        // ��ų �����͸� �����մϴ�.
        public void SetData(MagicBookData data, Action<SkillScrollSlot> action)
        {
            magicBookData = data;
            rerollAction = action;

            Texture2D texture = null;
            if (data.isActive)
                texture = ObjectManager.Instance.Load<Texture2D>($"Sprites/MagicBookIcon/Active/{data.iconResource}");
            else
                texture = ObjectManager.Instance.Load<Texture2D>($"Sprites/MagicBookIcon/Passive/{data.iconResource}");

            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                skillIcon.sprite = sprite;
            }
            skillName.SetText(data.name);
            skillDesc.SetText(data.desc);
            if (ColorUtility.TryParseHtmlString(data.colorcode, out Color color))
            {
                bookImg.color = color;
            }
        }

        // ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickReRollBtn()
        {
            if (rerollAction == null) return;
            rerollAction(this);
        }

        // ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickSelectBtn()
        {
            BuffManager.Instance.ChooseMagicBook(magicBookData);
        }


    }
}