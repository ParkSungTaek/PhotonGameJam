// 2024/07/28 [�̼���]
// �ƿ����� Scene

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