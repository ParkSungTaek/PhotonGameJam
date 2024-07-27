using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace Client
{
    public class OptionPopupPage : UI_Popup
    {
        [SerializeField] private Slider bgmBar = null; // 배경음악 Slider
        [SerializeField] private Slider sfxBar = null; // 효과음 Slider
        
        void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            bgmBar.value = AudioManager.Instance.GetVolume(SystemEnum.Sounds.BGM);
            sfxBar.value = AudioManager.Instance.GetVolume(SystemEnum.Sounds.SFX);

            bgmBar.onValueChanged.AddListener(volume => { AudioManager.Instance.SetVolume(SystemEnum.Sounds.BGM, volume); });
            sfxBar.onValueChanged.AddListener(volume => { AudioManager.Instance.SetVolume(SystemEnum.Sounds.SFX, volume); });
        }

    }
}