using UnityEngine;

[CreateAssetMenu]
public class BuyItem : Item
{
    public Item item;

    public override bool CanBuy(UnitSpawner spawner)
    {
        return true;
    }

    public override void Buy(UnitSpawner spawner)
    {
        //player.AddItem(item);
        Debug.Log("구매완료");
    }
}
