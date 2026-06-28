using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{
    public void ReturnToMenu()
    {
        GameManager.Instance.ResetCycle();

        PlayerDataManager.Instance.AddMoney(GameFlowManager.Instance.killCount);

        SceneManager.LoadScene("GameMenu");
    }
}