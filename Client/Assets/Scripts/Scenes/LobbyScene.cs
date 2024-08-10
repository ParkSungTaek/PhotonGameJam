// 2024/07/28 [�̼���]
// �ƿ����� Scene

using UnityEngine;

namespace Client
{
    public class LobbyScene : MonoBehaviour
    {
        private void Start()
        {
            // ó�� ������ �ƴ� ���
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

            // ó�� ������ ���
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