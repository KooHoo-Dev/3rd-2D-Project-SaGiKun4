using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] private Canvas optionCanvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        optionCanvas.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void ShowOption()
    {
        optionCanvas.gameObject.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}