using UnityEngine;

public class DefencePoint : InteractionPoint
{
    public override string InteractionText { get; } = "Defence";
    public override void Execute()
    {
        SceneLoader.Instance.LoadScene("Defence");
    }
}
