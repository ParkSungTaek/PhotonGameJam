// 2024/07/28 [이서연]
// 마법사 꾸미기 페이지 탭 슬롯

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static Client.SystemEnum;

namespace Client
{
    public class PlayerFaceUI : MonoBehaviour
    {
        [SerializeField] private Image body   = null; // 몸 UI
        [SerializeField] private Image face   = null; // 얼굴 UI
        [SerializeField] private Image hair   = null; // 머리카락 UI
        [SerializeField] private Image weapon = null; // 무기 UI
        [SerializeField] private Image cape   = null; // 망토 UI
        [SerializeField] private Image hat    = null; // 모자 UI

        private Dictionary<DecoType, DecoData> items = new(); // 꾸미기 아이템 정보

        public void Awake()
        {
            items.Clear();
            for (int i = 0; i < (int)DecoType.MaxCount; ++i)
            {
                items.Add((DecoType)i, new DecoData());
            }
        }

        // 캐릭터 외형을 갱신합니다.
        private void RefreshDeco()
        {
            if(items.ContainsKey(DecoType.Face))
            {
                DecoData faceData = items[DecoType.Face];
                Texture2D faceTexture = Resources.Load<Texture2D>($"Sprites/Characters/{faceData._type}/{faceData._resource}");
                if (faceTexture != null)
                {
                    Sprite faceSprite = Sprite.Create(faceTexture, new Rect(0, 0, faceTexture.width, faceTexture.height), new Vector2(0.5f, 0.5f));
                    face.sprite = faceSprite;
                }
            }

            if (items.ContainsKey(DecoType.Body))
            {
                DecoData bodyData = items[DecoType.Body];
                Texture2D bodyTexture = Resources.Load<Texture2D>($"Sprites/Characters/{bodyData._type}/{bodyData._resource}");
                if (bodyTexture != null)
                {
                    Sprite bodySprite = Sprite.Create(bodyTexture, new Rect(0, 0, bodyTexture.width, bodyTexture.height), new Vector2(0.5f, 0.5f));
                    body.sprite = bodySprite;
                }
            }

            if (items.ContainsKey(DecoType.Hair))
            {
                DecoData data = items[DecoType.Hair];
                Texture2D texture = Resources.Load<Texture2D>($"Sprites/Characters/{data._type}/{data._resource}");
                if (texture != null)
                {
                    Sprite bodySprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
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

            if (items.ContainsKey(DecoType.Hat))
            {
                DecoData bodyData = items[DecoType.Hat];
                Texture2D bodyTexture = ObjectManager.Instance.Load<Texture2D>($"Sprites/Characters/{bodyData._type}/{bodyData._resource}");
                if (bodyTexture != null)
                {
                    Sprite bodySprite = Sprite.Create(bodyTexture, new Rect(0, 0, bodyTexture.width, bodyTexture.height), new Vector2(0.5f, 0.5f));
                    hat.sprite = bodySprite;
                }
            }

            if (items.ContainsKey(DecoType.Cape))
            {
                DecoData bodyData = items[DecoType.Cape];
                Texture2D bodyTexture = ObjectManager.Instance.Load<Texture2D>($"Sprites/Characters/{bodyData._type}/{bodyData._resource}");
                if (bodyTexture != null)
                {
                    Sprite bodySprite = Sprite.Create(bodyTexture, new Rect(0, 0, bodyTexture.width, bodyTexture.height), new Vector2(0.5f, 0.5f));
                    cape.sprite = bodySprite;
                }
            }
        }

        // 캐릭터 외형을 한번에 세팅합니다.
        public void SetPlayerDeco(Dictionary<DecoType, DecoData> decos)
        {
            items = decos;
            RefreshDeco();
        }

        // 캐릭터 외형을 하나씩 세팅합니다.
        public void SetPlayerDeco(DecoType type, DecoData index)
        {
            items[type] = index;
            RefreshDeco();
        }
    }
}