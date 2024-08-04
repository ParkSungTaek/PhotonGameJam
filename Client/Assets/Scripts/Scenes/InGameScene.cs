// 2024/07/28 [�̼���]
// �ΰ��� Scene

using Fusion;
using UnityEngine;
namespace Client
{
    public class InGameScene : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Instance.ShowSceneUI<InGamePage>();

            ChatManager.Instance.SetState(SystemEnum.OnlineState.Game);
            //AudioManager.Instance.PlayLoop("BGM");
        }
    }
}