using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventurePoint : InteractionPoint
{
    public override string InteractionText
    {
        get
        {
            return "Adventure";
        }
    }

    public override void Execute()
    {
        SceneManager.LoadScene("Adventure");
    }
}
