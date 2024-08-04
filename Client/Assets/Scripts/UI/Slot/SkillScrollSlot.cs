// 2024/07/28 [�̼���]
// ��ų ������ UI

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Client
{
    public class SkillScrollSlot : UI_Base
    {
        [SerializeField] private Image    skillIcon = null; // ��ų ������
        [SerializeField] private Button   rerollBtn = null; // ���� ��ư
        [SerializeField] private TMP_Text skillName = null; // ��ų �̸�
        [SerializeField] private TMP_Text skillDesc = null; // ��ų ����
        private Action<SkillScrollSlot> rerollAction = null;
        public override void Init()
        {
            base.Init();
            rerollBtn.onClick.AddListener(OnClickReRollBtn);
        }

        // ��ų �����͸� �����մϴ�.
        public void SetData(MagicBookData data, Action<SkillScrollSlot> action)
        {
            rerollAction = action;
            Texture2D texture = ObjectManager.Instance.Load<Texture2D>($"Sprites/MagicBookIcon/{data.iconResource}");
            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                skillIcon.sprite = sprite;
            }
            skillName.SetText(data.name);
            skillDesc.SetText(data.desc);
        }

        // ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickReRollBtn()
        {
            if (rerollAction == null) return;
            rerollAction(this);
        }
    }
}