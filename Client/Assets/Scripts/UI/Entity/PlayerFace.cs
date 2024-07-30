// 2024/07/28 [이서연]
// 마법사 꾸미기 페이지 탭 슬롯

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static Client.SystemEnum;

namespace Client
{
    public class PlayerFace : UI_Base
    {
        [SerializeField] private Image body = null; // 몸 UI
        [SerializeField] private Image face = null; // 얼굴 UI

        private Dictionary<DecoType, DecoData> items = new(); // 꾸미기 아이템 정보

        public void Awake()
        {
            base.Init();
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