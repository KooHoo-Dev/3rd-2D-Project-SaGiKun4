using UnityEngine;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] private Canvas optionCanvas;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        optionCanvas.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        bgmSlider.value = AudioManager.Instance.bgmVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;
    }

    public void StartGame()
    {
        SceneLoader.Instance.LoadScene("GameMenu");
    }

    public void ShowOption()
    {
        optionCanvas.gameObject.SetActive(true);
    }

    public void HideOption()
    {
        optionCanvas.gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}