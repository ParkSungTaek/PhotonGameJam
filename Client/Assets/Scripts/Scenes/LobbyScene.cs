// 2024/07/28 [이서연]
// 아웃게임 Scene

using UnityEngine;

namespace Client
{
    public class LobbyScene : MonoBehaviour
    {
        private void Start()
        {
            // 처음 접속이 아닌 경우
            if( MyInfoManager.Instance.GetTuto() )
            {
                GameObject networkRunner = GameObject.Find("NetworkRunner");
                if (networkRunner != null)
                {
                    NetworkHandler networkHandler = networkRunner.GetComponent<NetworkHandler>();

                    if (networkHandler != null)
                    {
                        networkHandler.StartGame();
                        NetworkManager.Instance.NetworkHandler = networkHandler;
                    }
                }

                UIManager.Instance.ShowSceneUI<LobbyPage>();
                return;
            }

            // 처음 접속인 경우
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