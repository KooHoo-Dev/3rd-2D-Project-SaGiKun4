using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Item/DefenceSpawn")]
public class DefenceSpawn : Item
{
    public Unit unitPrefab;

    public bool CanBuy(UnitSpawner spawner)
    {
        return spawner.FindEmptySlot() != -1;
    }

    public override void Buy(UnitSpawner spawner)
    {
        spawner.Spawn(unitPrefab);
        Debug.Log("구매완료(소환)");
    }
}
