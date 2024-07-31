// 2024/07/28 [이서연]
// 옵션 UI 팝업 페이지

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class OptionPopupPage : UI_Popup
    {
        [SerializeField] private Slider     bgmBar     = null; // 배경음악 Slider
        [SerializeField] private Slider     sfxBar     = null; // 효과음 Slider
        [SerializeField] private Toggle     audioBtn   = null; // 오디오 설정 버튼
        [SerializeField] private Toggle     videoBtn   = null; // 화면 설정 버튼
        [SerializeField] private Toggle     fullBtn    = null; // 풀 스크린 버튼
        [SerializeField] private Dropdown   resolution = null; // 해상도 설정
        [SerializeField] private GameObject audioGroup = null; // 오디오 설정 그룹
        [SerializeField] private GameObject videoGroup = null; // 화면 설정 그룹

        private List<Resolution> resolutions = new(); // 해상도 정보
        private int resolutionIdx = 0;
        private FullScreenMode screenMode;

        public override void Init()
        {
            base.Init();
            InitDropDown();
            InitSlider();
            InitButton();
            audioBtn.isOn = true;
            fullBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
        }

        public override void ReOpenPopupUI()
        {
            base.ReOpenPopupUI();
            InitDropDown();
            InitSlider();
            InitButton();
            audioBtn.isOn = true;
            fullBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
        }

        // 드롭다운을 초기화합니다.
        public void InitDropDown()
        {
            resolutions.AddRange(Screen.resolutions);
            resolution.options.Clear();

            for(int i =0; i<resolutions.Count; ++i )
            { 
                Dropdown.OptionData option = new();
                option.text = resolutions[i].width + "x" + resolutions[i].height + "  " + resolutions[i].refreshRate + "hz";
                resolution.options.Add(option);

                if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                {
                    resolution.value = i;
                }
            }
            resolution.RefreshShownValue();
        }

        // 슬라이더를 초기화합니다.
        public void InitSlider()
        {
            //bgmBar.value = AudioManager.Instance.GetVolume(SystemEnum.Sounds.BGM);
            //sfxBar.value = AudioManager.Instance.GetVolume(SystemEnum.Sounds.SFX);

            //bgmBar.onValueChanged.AddListener(volume => { AudioManager.Instance.SetVolume(SystemEnum.Sounds.BGM, volume); });
            //sfxBar.onValueChanged.AddListener(volume => { AudioManager.Instance.SetVolume(SystemEnum.Sounds.SFX, volume); });
        }

        // 버튼을 초기화합니다.
        public void InitButton()
        {
            audioBtn.onValueChanged.AddListener(OnClickAudioBtn);
            videoBtn.onValueChanged.AddListener(OnClickVideoBtn);
            fullBtn.onValueChanged.AddListener(OnClickFullScreenBtn);
        }

        // 오디오 버튼을 눌렀을 때 호출됩니다.
        private void OnClickAudioBtn(bool isOn)
        {
            audioGroup.SetActive(isOn);
        }

        // 비디오 버튼을 눌렀을 때 호출됩니다.
        private void OnClickVideoBtn(bool isOn)
        {
            videoGroup.SetActive(isOn);
        }

        // 전체 화면 버튼을 눌렀을 때 호출됩니다.
        public void OnClickFullScreenBtn(bool isFull)
        {
            screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        }

        // 해상도가 선택되었을 때 호출됩니다.
        public void OnResolutionChange(int value)
        {
            resolutionIdx = value;
            Screen.SetResolution(resolutions[value].width, resolutions[value].height, screenMode);
        }


    }
}