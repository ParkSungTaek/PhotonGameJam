using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Client
{
    public class LoadingScene : MonoBehaviour
    {
        private float       loadingValue = 0;    // ���� �ε� ���൵
        private LoadingPage loadingUI    = null; // �ε� UI

        private const float loadingTime  = 1.0f; // �ε� �ð�

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

        // Scene�� �ε��մϴ�.
        IEnumerator LoadScene(SystemEnum.Scenes destScene)
        {
            yield return null;
            AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)destScene); // �񵿱� Scene �ε� ( �ε��� Scene �̸� )
            op.allowSceneActivation = false;
            yield return new WaitForSecondsRealtime(loadingTime);
            op.allowSceneActivation = true;
        }
    }
}