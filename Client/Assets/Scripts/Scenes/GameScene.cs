using UnityEngine;


namespace Client
{
    /// <summary>
    /// Scene �ʱ�ȭ class
    /// </summary>
    public class GameScene : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Instance.ShowSceneUI<UI_GameScene>();
        }
    }
}