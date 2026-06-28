using UnityEngine;
using Jay;
using UnityEngine.InputSystem;

public class GameManager : SingletonBase<GameManager>
{
    public bool IsAdventureClear { get; set; } = false;
    public int DefenceScore { get; private set; }

    public void QuitAdventure()
    {
        IsAdventureClear = true;
    }

    public void ResetCycle()
    {
        IsAdventureClear = false;
    }

    public void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            Debug.Log(IsAdventureClear);
        }
    }

}
