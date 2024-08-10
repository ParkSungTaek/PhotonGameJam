// 2024/07/28 [이서연]
// 인게임 Scene

using Fusion;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static Client.SystemEnum;

namespace Client
{
    public class InGameScene : MonoBehaviour
    {
        GameState _gameState = GameState.Wait;
        NetworkRunner _networkRunner;
        MatchingPage _matchingPage;
        public InGamePage _inGamePage { get; set; }

        private void Start()
        {
            ChatManager.Instance.SetState(SystemEnum.OnlineState.Game);

            GameObject targetObject = GameObject.Find("NetworkRunner");
            if (targetObject != null)
            {
                NetworkHandler networkHandler = targetObject.GetComponent<NetworkHandler>();

                if (networkHandler != null)
                {
                    NetworkManager.Instance.NetworkHandler = networkHandler;
                    _networkRunner = NetworkManager.Instance.NetworkHandler._runner;
                }
            }
            //NetworkManager.Instance.NetworkHandler._runner.AddCallbacks()
            //AudioManager.Instance.PlayLoop("BGM");
        }

        private void Update()
        {
            if(_networkRunner == null)
            {
                return;
            }

            if(_gameState == GameState.Wait)
            {
                if( _networkRunner.SessionInfo.PlayerCount >= 2 )
                {
                    if(EntityManager.Instance.MyPlayer == null)
                    {
                        return;
                    }

                    if(EntityManager.Instance.MyPlayer._matchingPage == null)
                    {
                        return;
                    }

                    EntityManager.Instance.MyPlayer._matchingPage.Back();
                    EntityManager.Instance.MyPlayer.PlayerInfo.IsInput = true;
                    _inGamePage = UIManager.Instance.ShowSceneUI<InGamePage>();
                    _gameState = GameState.Ready;
                }
            }
        }
    }
}