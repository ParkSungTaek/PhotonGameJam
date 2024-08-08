// 2024/07/28 [이서연]
// 마법사 꾸미기 페이지 탭 슬롯

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static Client.SystemEnum;
using TMPro;
using System.IO;

namespace Client
{
    public class PlayerFace : MonoBehaviour
    {
        [SerializeField] private TMP_Text       name   = null; // 닉네임 UI
        [SerializeField] private Animator       hpAnim = null; // 체력바 애니메이션
        [SerializeField] private SpriteRenderer body   = null; // 몸 UI
        [SerializeField] private SpriteRenderer face   = null; // 얼굴 UI
        [SerializeField] private SpriteRenderer hair   = null; // 얼굴 UI
        [SerializeField] private SpriteRenderer weapon   = null; // 얼굴 UI
        [SerializeField] private SpriteRenderer hpBar  = null; // HP 바

        private Dictionary<DecoType, DecoData> items = new(); // 꾸미기 아이템 정보
        private string nickName = string.Empty;

        public void Awake()
        {
            items.Clear();
            for (int i = 0; i < (int)DecoType.MaxCount; ++i)
            {
                items.Add((DecoType)i, new DecoData());
            }
        }

        // 캐릭터 외형을 갱신합니다.
        public void RefreshDeco()
        {
            if(items.ContainsKey(DecoType.Face))
            {
                DecoData faceData = items[DecoType.Face];
                Texture2D faceTexture = ObjectManager.Instance.Load<Texture2D>($"Sprites/Characters/{faceData._type}/{faceData._resource}");
                if (faceTexture != null)
                {
                    Sprite faceSprite = Sprite.Create(faceTexture, new Rect(0, 0, faceTexture.width, faceTexture.height), new Vector2(0.5f, 0.5f));
                    face.sprite = faceSprite;
                }
            }

            if (items.ContainsKey(DecoType.Body))
            {
                DecoData bodyData = items[DecoType.Body];
                Texture2D bodyTexture = ObjectManager.Instance.Load<Texture2D>($"Sprites/Characters/{bodyData._type}/{bodyData._resource}");
                if (bodyTexture != null)
                {
                    Sprite bodySprite = Sprite.Create(bodyTexture, new Rect(0, 0, bodyTexture.width, bodyTexture.height), new Vector2(0.5f, 0.5f));
                    body.sprite = bodySprite;
                }
            }

            if (items.ContainsKey(DecoType.Hair))
            {
                DecoData bodyData = items[DecoType.Hair];
                Texture2D bodyTexture = ObjectManager.Instance.Load<Texture2D>($"Sprites/Characters/{bodyData._type}/{bodyData._resource}");
                if (bodyTexture != null)
                {
                    Sprite bodySprite = Sprite.Create(bodyTexture, new Rect(0, 0, bodyTexture.width, bodyTexture.height), new Vector2(0.5f, 0.5f));
                    hair.sprite = bodySprite;
                }
            }

            if (items.ContainsKey(DecoType.Weapon))
            {
                DecoData bodyData = items[DecoType.Weapon];
                Texture2D bodyTexture = ObjectManager.Instance.Load<Texture2D>($"Sprites/Characters/{bodyData._type}/{bodyData._resource}");
                if (bodyTexture != null)
                {
                    Sprite bodySprite = Sprite.Create(bodyTexture, new Rect(0, 0, bodyTexture.width, bodyTexture.height), new Vector2(0.5f, 0.5f));
                    weapon.sprite = bodySprite;
                }
            }

            if (name == null) return;
            name.SetText(nickName);
        }

        // 캐릭터 닉네임을 세팅합니다
        public void SetNickName(string nickName)
        {
            this.nickName = nickName;
        }

        // 체력을 세팅합니다
        public void SetHPBar(float value)
        {
            // TODO:이서연 애니메이션이 여유가 되면 하기.
            //hpAnim.SetFloat("HPValue", value);

            RectTransform hpRectTransform = hpBar.GetComponent<RectTransform>();
            Vector3 currentScale = hpRectTransform.localScale;
            currentScale.x = value;
            hpRectTransform.localScale = currentScale;
        }

        // 캐릭터 외형을 하나씩 세팅합니다.
        public void SetPlayerDeco(DecoType type, DecoData index)
        {
            items[type] = index;
        }
    }
}