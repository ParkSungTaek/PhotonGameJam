// 2024/07/28 [�̼���]
// ������ �ٹ̱� ������ �� ����

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static Client.SystemEnum;

namespace Client
{
    public class PlayerFace : UI_Base
    {
        [SerializeField] private Image body = null; // �� UI
        [SerializeField] private Image face = null; // �� UI

        private Dictionary<DecoType, DecoData> items = new(); // �ٹ̱� ������ ����

        public void Awake()
        {
            base.Init();
            items.Clear();
            for (int i = 0; i < (int)DecoType.MaxCount; ++i)
            {
                items.Add((DecoType)i, new DecoData());
            }
        }

        // ĳ���� ������ �����մϴ�.
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

        // ĳ���� ������ �ѹ��� �����մϴ�.
        public void SetPlayerDeco(Dictionary<DecoType, DecoData> decos)
        {
            items = decos;
            RefreshDeco();
        }

        // ĳ���� ������ �ϳ��� �����մϴ�.
        public void SetPlayerDeco(DecoType type, DecoData index)
        {
            items[type] = index;
            RefreshDeco();
        }
    }
}