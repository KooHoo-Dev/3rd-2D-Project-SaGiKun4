using UnityEngine;

public class DefencePoint : InteractionPoint
{
    public override void Execute()
    {
        SceneLoader.Instance.LoadScene("Defence");
    }
}
