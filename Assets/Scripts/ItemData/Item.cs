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
public abstract class Item : ScriptableObject
{
    public int Key; // 아이템을 찾을 Key
    public string Name; // 아이템의 명칭
    public int price;
    public Sprite Icon; // 아이템의 Sprite 아이콘
    public Sprite BirdSprite;
    public ItemTier Tier;
    public UnitAttack attackData;
    public AnimatorOverrideController overrideController;
    public Vector3 detectedArea;
    public abstract bool CanBuy();
    public abstract void Buy();
}