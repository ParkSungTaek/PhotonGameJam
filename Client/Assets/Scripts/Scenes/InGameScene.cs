// 2024/07/28 [�̼���]
// �ΰ��� Scene

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