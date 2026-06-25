using UnityEngine;

[CreateAssetMenu]
public class SpawnUnit : Item
{
    public Unit unitPrefab;

    public override bool CanBuy(UnitSpawner spawner)
    {
        return spawner.FindEmptySlot() != -1;
    }

    public override void Buy(UnitSpawner spawner)
    {
        spawner.Spawn(unitPrefab);
        Debug.Log("구매완료(소환)");
    }
}
