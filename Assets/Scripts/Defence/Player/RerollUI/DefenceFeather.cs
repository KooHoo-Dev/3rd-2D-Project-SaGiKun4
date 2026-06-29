using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Item/DefenceFeather")]
public class DefenceFeather : Item
{
    public override bool CanBuy()
    {
        return GameFlowManager.Instance.defencePoint >= price;
    }

    public override void Buy()
    {
        Debug.Log("구매완료" + Name);
        GameFlowManager.Instance.defencePoint = GameFlowManager.Instance.defencePoint - price;
        InventoryManager.Instance.Inventory.AddItem(this);
        ContentController.Instance.Refresh();
    }
}