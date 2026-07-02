using UnityEngine;
using TMPro;

public class TrainingManager : MonoBehaviour
{
    [SerializeField] private GameObject trainWindow;
    [SerializeField] private TMP_Text moneyUI;

    private void OnEnable()
    {
        UpdateMoneyUI();
    }

    public void TrainPower(int price)
    {
        PlayerDataManager.Instance.TrainPower(price);
        UpdateMoneyUI();
    }

    public void TrainHealth(int price)
    {
        PlayerDataManager.Instance.TrainHealth(price);
        UpdateMoneyUI();
    }

    public void TrainFeather(int price)
    {
        PlayerDataManager.Instance.TrainFeather(price);
        UpdateMoneyUI();
    }

    public void ShowTrainWindow()
    {
        trainWindow.SetActive(true);
    }

    public void HideTrainWindow()
    {
        trainWindow.SetActive(false);
    }

    public void UpdateMoneyUI()
    {
        moneyUI.text = $"남은 금액 : {PlayerDataManager.Instance.Money}";
    }
}
