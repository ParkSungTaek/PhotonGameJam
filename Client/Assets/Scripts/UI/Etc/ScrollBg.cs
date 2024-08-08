using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class ScrollBg : MonoBehaviour
    {
        [SerializeField] private Image bg1 = null; // 배경 1
        [SerializeField] private Image bg2 = null; // 배경 2

        public  float scrollSpeed = 0.0f; // 스크롤 속도

        private float         screenWidth = 0.0f; // 화면 너비
        private RectTransform transform1  = new(); // 배경1 Transform
        private RectTransform transform2  = new(); // 배경2 Transform

        void Start()
        {
            transform1 = bg1.GetComponent<RectTransform>();
            transform2 = bg2.GetComponent<RectTransform>();
            screenWidth = Screen.width;

            // 배경 2를 배경 1의 오른쪽에 위치시킴
            transform2.anchoredPosition = new Vector2(screenWidth, transform1.anchoredPosition.y);
        }

        void Update()
        {
            if (bg1 == null || bg2 == null) return;

            // 배경을 왼쪽으로 스크롤
            transform1.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;
            transform2.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

            // 첫 번째 배경이 화면 밖으로 나가면 두 번째 배경 뒤로 이동
            if (transform1.anchoredPosition.x <= -screenWidth)
            {
                transform1.anchoredPosition = new Vector2(transform2.anchoredPosition.x + screenWidth, transform1.anchoredPosition.y);
            }

            // 두 번째 배경이 화면 밖으로 나가면 첫 번째 배경 뒤로 이동
            if (transform2.anchoredPosition.x <= -screenWidth)
            {
                transform2.anchoredPosition = new Vector2(transform1.anchoredPosition.x + screenWidth, transform2.anchoredPosition.y);
            }
        }
    }
}