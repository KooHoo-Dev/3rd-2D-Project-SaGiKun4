using Jay;
using Study.Utilities;
using UnityEngine;

public class OptionManager : SingletonBase<OptionManager>
{
    [SerializeField] private GameObject optionCanvas;
    public float BgmVolume { get; private set; }
    public float SfxVolume { get; private set; }

    public int CurrentWidth { get; private set; }
    public int CurrentHeight { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        BgmVolume = AudioManager.Instance.bgmVolume;
        SfxVolume = AudioManager.Instance.sfxVolume;

        CurrentWidth = Screen.width;
        CurrentHeight = Screen.height;
    }

    private void Update()
    {
        if(SimpleInput.GetKeyDown(UnityEngine.InputSystem.Key.Escape) && 
           SceneFlowManager.Instance.CanOpenOption())
        {
            optionCanvas.SetActive(!optionCanvas.activeInHierarchy);
            Time.timeScale = optionCanvas.activeSelf ? 0f : 1f;
        }
    }

    public void CloseOption()
    {
        optionCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetBgmVolume(float value)
    {
        BgmVolume = value;
        AudioManager.Instance.SetBgmVolume(value);
    }

    public void SetSfxVolume(float value)
    {
        SfxVolume = value;
        AudioManager.Instance.SetSfxVolume(value);
    }

    public void SetResolution(int width, int height, bool fullscreen)
    {
        Screen.SetResolution(width, height, fullscreen);

        CurrentWidth = width;
        CurrentHeight = height;
    }

    public void SelectResolution(int index)
    {
        switch (index)
        {
            case 1:
                SetResolution(1024, 768, false);
                break;
            case 2:
                SetResolution(1280, 720, false);
                break;
            case 3:
                SetResolution(1366, 768, false);
                break;
            case 4:
                SetResolution(1920, 1080, false);
                break;
        }
    }

    public string GetResolutionText()
    {
        return $"{CurrentWidth}x{CurrentHeight}";
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        optionCanvas.SetActive(false);
        SceneLoader.Instance.LoadScene("MainMenu");
    }
}