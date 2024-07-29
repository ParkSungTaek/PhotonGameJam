using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace Client
{
    public class DecoTabSlot : MonoBehaviour
    {
        [SerializeField] private Image    tabIcon = null; // 꾸미기 탭 아이콘
        [SerializeField] private TMP_Text tabName = null; // 꾸미기 탭 이름

        private void Awake()
        {

        }
    }
}