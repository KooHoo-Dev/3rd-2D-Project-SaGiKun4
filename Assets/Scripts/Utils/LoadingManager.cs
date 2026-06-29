using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;

    public static string NextSceneName;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(NextSceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp(operation.progress / 0.9f, 0f, 1f);
            loadingSlider.value = progress;

            if (operation.progress >= 0.9f)
            {
                loadingSlider.value = 1f;

                yield return new WaitForSeconds(0.5f);

                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}