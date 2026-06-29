using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    public void ReturnToMenu()
    {
        GameManager.Instance.ResetCycle();

        PlayerDataManager.Instance.AddMoney(GameFlowManager.Instance.killCount);

        SceneLoader.Instance.LoadScene("GameMenu");
    }
}