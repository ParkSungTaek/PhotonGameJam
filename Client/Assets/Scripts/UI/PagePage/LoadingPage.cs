
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using Client;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Client
{
    public class LoadingPage : UI_Scene
    {
        [SerializeField] private TMP_Text percentText = null; // 로딩 텍스트
        [SerializeField] private Slider   loadingBar  = null; // 로딩 게이지
  
        public override void Init()
        {
            base.Init();
        }

        // 로딩바의 값을 갱신해줍니다.
        public void SetLoadingSlider( float value )
        {
            loadingBar.value = value;
            percentText.text = "Loading... " + Mathf.Min((Mathf.Floor(value * 100f)), 100) + "%";
        }
    }
}