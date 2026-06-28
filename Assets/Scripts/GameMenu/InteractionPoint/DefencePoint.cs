using UnityEngine;
using UnityEngine.SceneManagement;

public class DefencePoint : InteractionPoint
{
    public override string InteractionText
    {
        get
        {
            return "Defence";
        }
    }
    public override void Execute()
    {
        SceneManager.LoadScene("Defence");
    }
}
