// 2024/07/28 [�̼���]
// �ɼ� UI �˾� ������

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class OptionPopupPage : UI_Popup
    {
        [SerializeField] private Slider     bgmBar     = null; // ������� Slider
        [SerializeField] private Slider     sfxBar     = null; // ȿ���� Slider
        [SerializeField] private Slider     voiceBar   = null; // ���� ä�� Slider
        [SerializeField] private Toggle     audioBtn   = null; // ����� ���� ��ư
        [SerializeField] private Toggle     videoBtn   = null; // ȭ�� ���� ��ư
        [SerializeField] private Toggle     fullBtn    = null; // Ǯ ��ũ�� ��ư
        [SerializeField] private TMP_Text   bgmText    = null; // ������� Text
        [SerializeField] private TMP_Text   sfxText    = null; // ȿ���� Text
        [SerializeField] private TMP_Text   voiceText  = null; // ���� ä�� Text
        [SerializeField] private Dropdown   resolution = null; // �ػ� ����
        [SerializeField] private GameObject audioGroup = null; // ����� ���� �׷�
        [SerializeField] private GameObject videoGroup = null; // ȭ�� ���� �׷�

        private List<Resolution> resolutions = new(); // �ػ� ����
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

        // ��Ӵٿ��� �ʱ�ȭ�մϴ�.
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

        // �����̴��� �ʱ�ȭ�մϴ�.
        public void InitSlider()
        {
            bgmBar.value = AudioManager.Instance.GetVolume(SystemEnum.Sounds.BGM);
            sfxBar.value = AudioManager.Instance.GetVolume(SystemEnum.Sounds.SFX);
            voiceBar.value = AudioManager.Instance.GetVolume(SystemEnum.Sounds.Voice);

            bgmText.SetText(((int)(bgmBar.value * 100)).ToString() + "%");
            sfxText.SetText(((int)(sfxBar.value * 100)).ToString() + "%");
            voiceText.SetText(((int)(voiceBar.value * 100)).ToString() + "%");

            bgmBar.onValueChanged.AddListener(volume => { AudioManager.Instance.SetVolume(SystemEnum.Sounds.BGM, volume); bgmText.SetText(((int)(bgmBar.value * 100)).ToString() + "%"); });
            sfxBar.onValueChanged.AddListener(volume => { AudioManager.Instance.SetVolume(SystemEnum.Sounds.SFX, volume); sfxText.SetText(((int)(sfxBar.value * 100)).ToString() + "%"); });
            voiceBar.onValueChanged.AddListener(volume => { AudioManager.Instance.SetVolume(SystemEnum.Sounds.Voice, volume); voiceText.SetText(((int)(voiceBar.value * 100)).ToString() + "%"); });
        }

        // ��ư�� �ʱ�ȭ�մϴ�.
        public void InitButton()
        {
            audioBtn.onValueChanged.AddListener(OnClickAudioBtn);
            videoBtn.onValueChanged.AddListener(OnClickVideoBtn);
            fullBtn.onValueChanged.AddListener(OnClickFullScreenBtn);
        }

        // ����� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickAudioBtn(bool isOn)
        {
            audioGroup.SetActive(isOn);
        }

        // ���� ��ư�� ������ �� ȣ��˴ϴ�.
        private void OnClickVideoBtn(bool isOn)
        {
            videoGroup.SetActive(isOn);
        }

        // ��ü ȭ�� ��ư�� ������ �� ȣ��˴ϴ�.
        public void OnClickFullScreenBtn(bool isFull)
        {
            screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        }

        // �ػ󵵰� ���õǾ��� �� ȣ��˴ϴ�.
        public void OnResolutionChange(int value)
        {
            resolutionIdx = value;
            Screen.SetResolution(resolutions[value].width, resolutions[value].height, screenMode);
        }


    }
}