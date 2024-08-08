using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class ScrollBg : MonoBehaviour
    {
        [SerializeField] private Image bg1 = null; // ��� 1
        [SerializeField] private Image bg2 = null; // ��� 2

        public  float scrollSpeed = 0.0f; // ��ũ�� �ӵ�

        private float         screenWidth = 0.0f; // ȭ�� �ʺ�
        private RectTransform transform1  = new(); // ���1 Transform
        private RectTransform transform2  = new(); // ���2 Transform

        void Start()
        {
            transform1 = bg1.GetComponent<RectTransform>();
            transform2 = bg2.GetComponent<RectTransform>();
            screenWidth = Screen.width;

            // ��� 2�� ��� 1�� �����ʿ� ��ġ��Ŵ
            transform2.anchoredPosition = new Vector2(screenWidth, transform1.anchoredPosition.y);
        }

        void Update()
        {
            if (bg1 == null || bg2 == null) return;

            // ����� �������� ��ũ��
            transform1.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;
            transform2.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

            // ù ��° ����� ȭ�� ������ ������ �� ��° ��� �ڷ� �̵�
            if (transform1.anchoredPosition.x <= -screenWidth)
            {
                transform1.anchoredPosition = new Vector2(transform2.anchoredPosition.x + screenWidth, transform1.anchoredPosition.y);
            }

            // �� ��° ����� ȭ�� ������ ������ ù ��° ��� �ڷ� �̵�
            if (transform2.anchoredPosition.x <= -screenWidth)
            {
                transform2.anchoredPosition = new Vector2(transform1.anchoredPosition.x + screenWidth, transform2.anchoredPosition.y);
            }
        }
    }
}