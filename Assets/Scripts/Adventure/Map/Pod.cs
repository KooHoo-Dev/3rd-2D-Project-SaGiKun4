using UnityEngine;
public class Pod : InteractionPoint
{
    public override void Execute()
    {
        AdventureManager.Instance.QuitAdventure();
    }
}