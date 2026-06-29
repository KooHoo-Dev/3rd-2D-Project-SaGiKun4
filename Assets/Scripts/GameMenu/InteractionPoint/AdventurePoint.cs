using UnityEngine;

public class AdventurePoint : InteractionPoint
{
    public override string InteractionText { get; } = "Adventure";

    public override void Execute()
    {
        SceneLoader.Instance.LoadScene("Adventure");
    }
}
