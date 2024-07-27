// 2024/07/28 [�̼���]
// �ɼ� UI �˾� ������

using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    public class OptionPopupPage : UI_Popup
    {
        [SerializeField] private Slider bgmBar = null; // ������� Slider
        [SerializeField] private Slider sfxBar = null; // ȿ���� Slider
        
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