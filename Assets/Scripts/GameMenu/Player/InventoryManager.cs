using Jay;
using UnityEngine;

public class InventoryManager : SingletonBase<InventoryManager>
{
    [field:SerializeField] public Inventory Inventory { get; private set; } = new();
    [field: SerializeField] public Inventory TempInventory { get; private set;} = new();
}