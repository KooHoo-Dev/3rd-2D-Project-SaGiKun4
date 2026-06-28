using UnityEngine;

public class MoneyChecker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(PlayerDataManager.Instance.Money);
    }
}
