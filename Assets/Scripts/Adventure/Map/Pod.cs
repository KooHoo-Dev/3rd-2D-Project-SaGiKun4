using UnityEngine;
public class Pod : InteractionPoint
{
    public override string InteractionText { get; } = "AdventureExit";

    public override void Execute()
    {
        AdventureManager.Instance.QuitAdventure();
    }
}