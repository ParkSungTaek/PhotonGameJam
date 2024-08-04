// 2024/07/28 [이서연]
// 스킬 양피지 UI

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace Client
{
    public class SkillScrollSlot : UI_Base
    {
        [SerializeField] private Image    skillIcon = null; // 스킬 아이콘
        [SerializeField] private Button   rerollBtn = null; // 리롤 버튼
        [SerializeField] private TMP_Text skillName = null; // 스킬 이름
        [SerializeField] private TMP_Text skillDesc = null; // 스킬 설명
        private Action<SkillScrollSlot> rerollAction = null;
        public override void Init()
        {
            base.Init();
            rerollBtn.onClick.AddListener(OnClickReRollBtn);
        }

        // 스킬 데이터를 세팅합니다.
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

        // 리롤 버튼을 눌렀을 때 호출됩니다.
        private void OnClickReRollBtn()
        {
            if (rerollAction == null) return;
            rerollAction(this);
        }
    }
}