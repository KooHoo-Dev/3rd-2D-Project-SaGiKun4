using Jay;
using UnityEngine;

public class PlayerDataManager : SingletonBase<PlayerDataManager>
{
    [field: SerializeField] public int Money { get; private set; }
    [field: SerializeField] public int Power { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int Feather { get; private set; }
    [field: SerializeField] public int DefenceLevel { get; set; }

    public void TrainPower()
    {
        if (OddMoney(Power+1))
            Power++;
        else
            Debug.Log("재화 부족, 강화 실패");
    }

    public void TrainHealth()
    {
        if (OddMoney(Health+1))
            Health++;
        else
            Debug.Log("재화 부족, 강화 실패");
    }

    public void TrainFeather()
    {
        if (OddMoney(Feather+1))
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