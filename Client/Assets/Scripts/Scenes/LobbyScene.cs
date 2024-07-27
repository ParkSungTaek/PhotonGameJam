using UnityEngine;


namespace Client
{
    public class LobbyScene : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Instance.ShowSceneUI<TitlePage>();
        }
    }
}