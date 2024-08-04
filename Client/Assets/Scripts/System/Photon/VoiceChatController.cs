using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;

namespace Client
{
    public class VoiceChatController : MonoBehaviour
    {
        private Recorder recorder;
        private bool transmitEnable = false;

        private void Awake()
        {
            if (recorder == null)
            {
                recorder = GetComponent<Recorder>();
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (EntityManager.Instance.MyPlayer == null)
            {
                return;
            }

            if (recorder.VoiceDetector.Detected) 
            {
                EntityManager.Instance.MyPlayer.isSpeaking = true;
            }
            else
            {
                EntityManager.Instance.MyPlayer.isSpeaking = false;
            }

            if (Input.GetKey(KeyCode.P))
            {
                if (recorder.TransmitEnabled)
                {
                    recorder.TransmitEnabled = false;
                }
                else
                {
                    recorder.TransmitEnabled = true;
                }
            }
        }
    }
}
