
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
        enum Sliders
        {
            LoadingBar
        }
        enum Texts
        {
            PercentText,
        }

        public override void Init()
        {
            base.Init();
            Bind<Slider>(typeof(Sliders));
            Bind<TMP_Text>(typeof(Texts));

            Get<Slider>((int)Sliders.LoadingBar).value = 0;
        }

        // 로딩바의 값을 갱신해줍니다.
        public void SetLoadingSlider( float value )
        {
            Get<Slider>((int)Sliders.LoadingBar).value = value;
            Get<TMP_Text>((int)Texts.PercentText).text = "Loading... " + Mathf.Min((Mathf.Floor(value * 100f)), 100) + "%";
        }

    }
}