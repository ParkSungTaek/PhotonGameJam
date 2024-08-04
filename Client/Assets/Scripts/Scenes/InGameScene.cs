// 2024/07/28 [이서연]
// 인게임 Scene

using Fusion;
using UnityEngine;
namespace Client
{
    public class InGameScene : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Instance.ShowSceneUI<InGamePage>();
            //AudioManager.Instance.PlayLoop("BGM");

            //GameObject targetObject = GameObject.Find("NetworkRunner");
            //if (targetObject != null)
            //{
            //    NetworkHandler networkHandler = targetObject.GetComponent<NetworkHandler>();

            //    if (networkHandler != null)
            //    {
            //        networkHandler.StartGame(NetworkManager.Instance.mode);
            //    }
            //}
        }

        //private void Update() 
        //{
        //    if( EntityManager.Instance.MyPlayer )
        //    {
        //        if (Input.GetKey(KeyCode.T))
        //        {
        //            if (chatPage)
        //            {
        //                chatPage.Back();
        //                chatPage = null;
        //            } 
        //            else
        //            {
        //                chatPage = UIManager.Instance.ShowSceneUI<ChatPage>();
        //            }
        //        }
        //    }
        //}
    }
}