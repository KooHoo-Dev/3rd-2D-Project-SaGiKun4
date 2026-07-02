using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    [SerializeField] private GameObject trainWindow;
    public void TrainPower(int price)
    {
        PlayerDataManager.Instance.TrainPower(price);
    }

    public void TrainHealth(int price)
    {
        PlayerDataManager.Instance.TrainHealth(price);
    }

    public void TrainFeather(int price)
    {
        PlayerDataManager.Instance.TrainFeather(price);
    }

    public void ShowTrainWindow()
    {
        trainWindow.SetActive(true);
    }

    public void HideTrainWindow()
    {
        trainWindow.SetActive(false);
    }
}
