using UnityEngine;
using UnityEngine.SceneManagement;

public class DefencePoint : InteractionPoint
{
    public override void Execute()
    {
        SceneManager.LoadScene("Defence");
    }
}
