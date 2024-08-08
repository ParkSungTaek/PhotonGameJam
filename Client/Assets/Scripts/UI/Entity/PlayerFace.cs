// 2024/07/28 [�̼���]
// ������ �ٹ̱� ������ �� ����

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
        [SerializeField] private TMP_Text       name   = null; // �г��� UI
        [SerializeField] private Animator       hpAnim = null; // ü�¹� �ִϸ��̼�
        [SerializeField] private SpriteRenderer body   = null; // �� UI
        [SerializeField] private SpriteRenderer face   = null; // �� UI
        [SerializeField] private SpriteRenderer hair   = null; // �� UI
        [SerializeField] private SpriteRenderer weapon   = null; // �� UI
        [SerializeField] private SpriteRenderer hpBar  = null; // HP ��

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

        // ĳ���� �г����� �����մϴ�
        public void SetNickName(string nickName)
        {
            this.nickName = nickName;
        }

        // ü���� �����մϴ�
        public void SetHPBar(float value)
        {
            // TODO:�̼��� �ִϸ��̼��� ������ �Ǹ� �ϱ�.
            //hpAnim.SetFloat("HPValue", value);

            RectTransform hpRectTransform = hpBar.GetComponent<RectTransform>();
            Vector3 currentScale = hpRectTransform.localScale;
            currentScale.x = value;
            hpRectTransform.localScale = currentScale;
        }

        // ĳ���� ������ �ϳ��� �����մϴ�.
        public void SetPlayerDeco(DecoType type, DecoData index)
        {
            items[type] = index;
        }
    }
}