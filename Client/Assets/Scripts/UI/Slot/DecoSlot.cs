// 2024/07/28 [이서연]
// 마법사 꾸미기 페이지 탭 슬롯

using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Client
{
    public class DecoSlot : MonoBehaviour
    {
        [SerializeField] private Image    tabIcon = null; // 꾸미기 탭 아이콘
        [SerializeField] private TMP_Text tabName = null; // 꾸미기 탭 이름
        private void Awake()
        {
            
        }
    }
}