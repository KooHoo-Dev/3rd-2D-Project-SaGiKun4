using UnityEngine;
using UnityEngine.SceneManagement; // 씬 제어를 위한 기능들이 담겨있는 네임스페이스

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