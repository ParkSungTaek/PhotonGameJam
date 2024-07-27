
namespace Client
{
    public class SceneManager : Singleton<SceneManager>
    {
        private static SystemEnum.Scenes destScene = SystemEnum.Scenes.MaxCount;
        public SystemEnum.Scenes DestScene => destScene;
        private SceneManager() { }

        public void LoadScene(SystemEnum.Scenes scene, bool cacheClear = false, bool showLoading = true)
        {
            // UI popup 초기화
            UIManager.Instance.Clear();
            destScene = scene;
            // ResourceManager 초기화
            if (cacheClear)
            {
                ObjectManager.Instance.Clear();
                AudioManager.Instance.Clear();
            }
            if(showLoading)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene((int)SystemEnum.Scenes.Loading);
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)destScene);
        }
    }
}