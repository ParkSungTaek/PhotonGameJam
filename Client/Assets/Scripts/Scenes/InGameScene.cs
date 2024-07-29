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
            GameObject targetObject = GameObject.Find("NetworkRunner");

            if (targetObject != null)
            {
                BasicSpawner basicSpawner = targetObject.GetComponent<BasicSpawner>();

                if (basicSpawner != null)
                {
                    basicSpawner.StartGameMode(NetworkManager.Instance.mode);
                }
            }

            Debug.LogWarning("InGameScene ���� ��ũ��Ʈ Start �� �׽�Ʈ�ڵ� �������");
            CharPlayer charPlayer = FindAnyObjectByType<CharPlayer>();

            Pistol weapon = ObjectManager.Instance.Instantiate<Pistol>("Weapon/Pistol");
            charPlayer.SetWeaponBase(weapon);
            UIManager.Instance.ShowSceneUI<InGamePage>();
        }
    }
}