using UnityEngine;

public class TrainingManager : MonoBehaviour
{
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
}
