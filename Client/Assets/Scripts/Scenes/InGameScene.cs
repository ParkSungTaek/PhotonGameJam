// 2024/07/28 [이서연]
// 인게임 Scene

using Fusion;
using UnityEngine;
namespace Client
{
    public class InGameScene : MonoBehaviour
    {
        private bool _chatEnable = false;
        private ChatPage chatPage;
        private void Start()
        {
            GameObject targetObject = GameObject.Find("NetworkRunner");

            if (targetObject != null)
            {
                BasicSpawner basicSpawner = targetObject.GetComponent<BasicSpawner>();

                if (basicSpawner != null)
                {
                    basicSpawner.StartGameMode(NetworkManager.Instance.mode);
                }
            }

            Debug.LogWarning("InGameScene 시작 스크립트 Start 에 테스트코드 살아있음");
            Player charPlayer = FindAnyObjectByType<Player>();

            //Pistol weapon = ObjectManager.Instance.Instantiate<Pistol>("Weapon/Pistol");
            //charPlayer.SetWeaponBase(weapon);
            //UIManager.Instance.ShowSceneUI<InGamePage>();
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