using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Item/DefenceSpawn")]
public class DefenceSpawn : Item
{
    public Unit unitPrefab;

    public override bool CanBuy()
    {
        return (UnitSpawner.Instance.FindEmptySlot() != -1) && (GameFlowManager.Instance.defencePoint >= price);
    }

    public override void Buy()
    {
        GameFlowManager.Instance.defencePoint = GameFlowManager.Instance.defencePoint - price;
        UnitSpawner.Instance.Spawn(unitPrefab);
    }
}
