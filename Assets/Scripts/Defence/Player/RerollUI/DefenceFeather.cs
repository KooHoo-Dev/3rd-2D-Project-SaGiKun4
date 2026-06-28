using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Item/DefenceFeather")]
public class DefenceFeather : Item
{
    public bool CanBuy(UnitSpawner spawner)
    {
        return true;
    }

    public override void Buy(UnitSpawner spawner)
    {
        Debug.Log("구매완료" + Name);
        InventoryManager.Instance.Inventory.AddItem(this);
        ContentController.Instance.Refresh();
    }
}