using Jay;

public class GameManager : SingletonBase<GameManager>
{
    public bool IsAdventureClear { get; set; } = false;

    public void QuitAdventure()
    {
        IsAdventureClear = true;
    }

    public void ResetCycle()
    {
        IsAdventureClear = false;
    }
}
