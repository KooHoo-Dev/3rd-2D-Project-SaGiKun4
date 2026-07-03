using UnityEngine;

public class AdventurePoint : InteractionPoint
{
    public override void Execute()
    {
        SceneLoader.Instance.LoadScene("Adventure");
    }
}
