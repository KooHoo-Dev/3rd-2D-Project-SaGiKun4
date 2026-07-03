using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainingManager : MonoBehaviour
{
    private enum TrainType
    {
        None,
        Power,
        Health,
        Feather
    }
    [SerializeField] private GameObject trainWindow;
    [SerializeField] private TMP_Text moneyUI;
    private TrainType currentType;

    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private TMP_Text headText;
    [SerializeField] private TMP_Text featherText;
    [SerializeField] private Button trainButton;
    [SerializeField] private TMP_Text buttonText;
    private int price;

    private void OnEnable()
    {
        UpdateUI();
    }

    public void TrainPower()
    {
        PlayerDataManager.Instance.TrainPower();
        UpdateUI();
    }

    public void TrainHealth()
    {
        PlayerDataManager.Instance.TrainHealth();
        UpdateUI();
    }

    public void TrainFeather()
    {
        PlayerDataManager.Instance.TrainFeather();
        UpdateUI();
    }

    public void Train()
    {
        switch (currentType)
        {
            case TrainType.None:
                break;
            case TrainType.Power:
                TrainPower();
                break;
            case TrainType.Health:
                TrainHealth();
                break;
            case TrainType.Feather:
                TrainFeather();
                break;
        }
    }

    public void SelectTrain(int type)
    {
        currentType = (TrainType)type;
        UpdateUI();
    }

    public void ShowTrainWindow()
    {
        trainWindow.SetActive(true);
    }

    public void HideTrainWindow()
    {
        trainWindow.SetActive(false);
    }

    public void UpdateUI()
    {
        moneyUI.text = $"남은 금액 : {PlayerDataManager.Instance.Money}";
        bodyText.text = $"현재 +{PlayerDataManager.Instance.Health} : {90 + (25 * PlayerDataManager.Instance.Health)}\n" + 
                        $"다음 레벨 +{PlayerDataManager.Instance.Health+1} : {90 + (25 * (PlayerDataManager.Instance.Health+1))}";
        headText.text = $"현재 +{PlayerDataManager.Instance.Power} : {3 + (7 * PlayerDataManager.Instance.Power)}\n" + 
                        $"다음 레벨 +{PlayerDataManager.Instance.Power+1} : {3 + (7 * (PlayerDataManager.Instance.Power+1))}";
        featherText.text = $"현재 +{PlayerDataManager.Instance.Feather} : {40 + (40 * PlayerDataManager.Instance.Feather)}\n" + 
                           $"다음 레벨 +{PlayerDataManager.Instance.Feather+1} : {40 + (40 * (PlayerDataManager.Instance.Feather+1))}";
        
        switch (currentType)
        {
            case TrainType.Power:
                price = PlayerDataManager.Instance.Power+1;
                break;
            case TrainType.Health:
                price = PlayerDataManager.Instance.Health+1;
                break;
            case TrainType.Feather:
                price = PlayerDataManager.Instance.Feather+1;
                break;
        }

        if(price <= 4)
        {
            buttonText.text = $"강화비용 : {price}";
            trainButton.enabled = true;
        }
        else
        {
            buttonText.text = "최대강화입니다.";
            trainButton.enabled = false;
        }
    }
}
