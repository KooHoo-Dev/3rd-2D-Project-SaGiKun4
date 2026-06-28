using Jay;
using UnityEngine;

public class PlayerDataManager : SingletonBase<PlayerDataManager>
{
    [field: SerializeField]
    public int Money { get; private set; }

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