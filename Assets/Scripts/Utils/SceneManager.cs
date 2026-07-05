using Jay;

public class SceneFlowManager : SingletonBase<SceneFlowManager>
{
    public enum SceneType
    {
        MainMenu,
        GameMenu,
        Adventure,
        Defense,
        Loading
    }

    public SceneType CurrentScene { get; private set; } = SceneType.MainMenu;

    public void ChangeScene(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenu":
                CurrentScene = SceneType.MainMenu;
                break;

            case "GameMenu":
                CurrentScene = SceneType.GameMenu;
                break;

            case "Adventure":
                CurrentScene = SceneType.Adventure;
                break;

            case "Defence":
                CurrentScene = SceneType.Defense;
                break;

            case "LoadingScene":
                CurrentScene = SceneType.Loading;
                break;
        }
    }

    public bool CanOpenOption()
    {
        if (CurrentScene == SceneType.Loading || CurrentScene == SceneType.MainMenu)
            return false;

        return true;
    }
}
