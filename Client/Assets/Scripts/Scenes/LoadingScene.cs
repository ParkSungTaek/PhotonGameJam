using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Client
{
    public class LoadingScene : MonoBehaviour
    {
        private float       loadingValue = 0;    // 현재 로딩 진행도
        private LoadingPage loadingUI    = null; // 로딩 UI

        private const float loadingTime  = 1.0f; // 로딩 시간

        private void Start()
        {
            loadingUI = UIManager.Instance.ShowSceneUI<LoadingPage>();
            StartCoroutine(LoadScene(SceneManager.Instance.DestScene));
        }

        void Update()
        {
            loadingValue += Time.deltaTime;
            loadingUI.SetLoadingSlider(loadingValue);
        }

        // Scene을 로딩합니다.
        IEnumerator LoadScene(SystemEnum.Scenes destScene)
        {
            yield return null;
            AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)destScene); // 비동기 Scene 로딩 ( 로딩할 Scene 이름 )
            op.allowSceneActivation = false;
            yield return new WaitForSecondsRealtime(loadingTime);
            op.allowSceneActivation = true;
        }
    }
}