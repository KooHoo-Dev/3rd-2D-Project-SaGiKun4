using Jay;
using UnityEngine;

public class PlayerDataManager : SingletonBase<PlayerDataManager>
{
    [field: SerializeField] public int Money { get; private set; }
    [field: SerializeField] public int Power { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int Feather { get; private set; }
    
    public void TrainPower(int price)
    {
        if (OddMoney(price))
            Power++;
        else
            Debug.Log("재화 부족, 강화 실패");
    }

    public void TrainHealth(int price)
    {
        if (OddMoney(price))
            Health++;
        else
            Debug.Log("재화 부족, 강화 실패");
    }

    public void TrainFeather(int price)
    {
        if (OddMoney(price))
            Feather++;
        else
            Debug.Log("재화 부족, 강화 실패");
        
    }

    public void AddMoney(int amount)
    {
        Money += amount;
    }

    public bool OddMoney(int amount)
    {
        if (Money < amount)
            return false;

        Money -= amount;
        return true;
    }
}