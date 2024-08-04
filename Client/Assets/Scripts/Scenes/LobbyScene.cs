// 2024/07/28 [이서연]
// 아웃게임 Scene

using UnityEngine;

namespace Client
{
    public class LobbyScene : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Instance.ShowSceneUI<TitlePage>();
        }
    }
}