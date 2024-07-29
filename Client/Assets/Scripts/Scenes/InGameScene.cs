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
            CharPlayer charPlayer = FindAnyObjectByType<CharPlayer>();

            Pistol weapon = ObjectManager.Instance.Instantiate<Pistol>("Weapon/Pistol");
            charPlayer.SetWeaponBase(weapon);
            UIManager.Instance.ShowSceneUI<InGamePage>();
        }
    }
}