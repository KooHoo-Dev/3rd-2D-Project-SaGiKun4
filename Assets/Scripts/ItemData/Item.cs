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

[CreateAssetMenu(fileName = "InventoryItem",menuName = "SAO/InventoryItem", order = 1)]
public abstract class Item : ScriptableObject
{
    public int Key; // 아이템을 찾을 Key
    public ItemTier Tier;
    public string Name; // 아이템의 명칭
    public string Description; // 아이템의 설명
    public Sprite Icon; // 아이템의 Sprite 아이콘
    public int Price; // 아이템의 가격
    public int Count; // 아이템의 가격
    public UnitAttack attackData;

    public abstract bool CanBuy(UnitSpawner spawner); // 나중에 돈 뺄꺼임
    public abstract void Buy(UnitSpawner spawner);
}