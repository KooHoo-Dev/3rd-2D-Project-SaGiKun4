using UnityEngine;

public enum ItemTier
{
    Unit,
    Tier1,
    Tier2,
    Tier3,
    Tier4
}

public enum AttackType
{
    Default,
    Splash
}

[CreateAssetMenu(fileName = "InventoryItem",menuName = "Item/InventoryItem")]
public class Item : ScriptableObject
{
    public int Key; // 아이템을 찾을 Key
    public string Name; // 아이템의 명칭
    public string Description; // 아이템의 설명
    public Sprite Icon; // 아이템의 Sprite 아이콘
    public ItemTier Tier;
    public UnitAttack attackData;

    public virtual void Buy(UnitSpawner unitSpawner) { }
}