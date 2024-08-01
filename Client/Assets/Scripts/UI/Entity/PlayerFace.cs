// 2024/07/28 [�̼���]
// ������ �ٹ̱� ������ �� ����

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static Client.SystemEnum;
using TMPro;

namespace Client
{
    public class PlayerFace : MonoBehaviour
    {
        [SerializeField] private TMP_Text       name = null; // �г��� UI
        [SerializeField] private SpriteRenderer body = null; // �� UI
        [SerializeField] private SpriteRenderer face = null; // �� UI

        private Dictionary<DecoType, DecoData> items = new(); // �ٹ̱� ������ ����
        private string nickName = string.Empty;

        public void Awake()
        {
            items.Clear();
            for (int i = 0; i < (int)DecoType.MaxCount; ++i)
            {
                items.Add((DecoType)i, new DecoData());
            }
        }

        // ĳ���� ������ �����մϴ�.
        public void RefreshDeco()
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

            if (name == null) return;
            name.SetText(nickName);
        }

        // ĳ���� �г����� �����մϴ�
        public void SetNickName(string nickName)
        {
            this.nickName = nickName;
        }

        // ĳ���� ������ �ϳ��� �����մϴ�.
        public void SetPlayerDeco(DecoType type, DecoData index)
        {
            items[type] = index;
        }
    }
}