using Jay;
using UnityEngine;

public class TutorialManager : SingletonBase<TutorialManager>
{
    public enum TutorialState
    {
        None,
        GameMenu_Adventure,
        Adventure,
        GameMenu_Defence,
        Defence,
        GameMenu_Forge,
        Finish
    }

    public TutorialState CurrentState = TutorialState.None;
}