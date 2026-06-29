using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Jay;

public class SceneLoader : SingletonBase<SceneLoader>
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private Canvas fadeCanvas;

    [SerializeField] private float fadeDuration = 0.3f;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        yield return FadeOut();

        LoadingManager.NextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "LoadingScene")
        {
            StartCoroutine(FadeIn());
        }
    }

    public IEnumerator FadeOut()
    {
        fadeCanvas.gameObject.SetActive(true);
        Color color = fadeImage.color;

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    public IEnumerator FadeIn()
    {
        fadeCanvas.gameObject.SetActive(true);
        Color color = fadeImage.color;

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
        fadeCanvas.gameObject.SetActive(false);
    }
}