// 2024/07/28 [�̼���]
// �ε� UI ������

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using Client;

namespace Client
{
    public class LoadingPage : UI_Scene
    {
        [SerializeField] private TMP_Text percentText = null; // �ε� �ؽ�Ʈ
        [SerializeField] private Slider   loadingBar  = null; // �ε� ������
  
        public override void Init()
        {
            base.Init();
        }

        // �ε����� ���� �������ݴϴ�.
        public void SetLoadingSlider( float value )
        {
            loadingBar.value = value;
            percentText.text = "Loading... " + Mathf.Min((Mathf.Floor(value * 100f)), 100) + "%";
        }
    }
}