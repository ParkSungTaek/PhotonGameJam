// 2024/07/28 [이서연]
// 아웃게임 Scene

using UnityEngine;

namespace Client
{
    public class LobbyScene : MonoBehaviour
    {
        private void Start()
        {
            if( MyInfoManager.Instance.GetTuto() )
            {
                UIManager.Instance.ShowSceneUI<LobbyPage>();
                return;
            }

            UIManager.Instance.ShowSceneUI<TitlePage>();

            GameObject targetObject = GameObject.Find("NetworkRunner");
            if (targetObject != null)
            {
                NetworkHandler networkHandler = targetObject.GetComponent<NetworkHandler>();

                if (networkHandler != null)
                {
                    networkHandler.StartGame();
                    NetworkManager.Instance.NetworkHandler = networkHandler;
                }
            }
        }
    }
}