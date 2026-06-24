using UnityEngine;
using UnityEngine.SceneManagement;

public class AdventurePoint : InteractionPoint
{
    public override void Execute()
    {
        SceneManager.LoadScene("Adventure");
    }
}
