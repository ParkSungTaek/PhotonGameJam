// 2024/07/28 [이서연]
// 인게임 Scene

using UnityEngine;
namespace Client
{
    public class InGameScene : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Instance.ShowSceneUI<InGamePage>();
        }
    }
}