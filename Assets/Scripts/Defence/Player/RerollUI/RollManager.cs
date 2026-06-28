using System.Collections.Generic;
using UnityEngine;

public class RollManager : MonoBehaviour
{
    [SerializeField] private Item[] allItems;
    [SerializeField] private ItemButtonBehaviour[] shopButtons;

    private readonly Dictionary<ItemTier, List<Item>> tierItems = new();

    private void OnEnable()
    {
        InitializeItems();
    }

    private void InitializeItems()
    {
        tierItems.Clear();

        foreach (Item defenceItem in allItems)
        {
            if (!UnlockManager.Instance.IsUnlocked(defenceItem))
                continue;

            if (!tierItems.ContainsKey(defenceItem.Tier))
            {
                tierItems.Add(defenceItem.Tier, new List<Item>());
            }

            tierItems[defenceItem.Tier].Add(defenceItem);
        }
    }

    private ItemTier RollTier()
    {
        int roll = Random.Range(0, 100);

        if (roll < 35)
            return ItemTier.Unit;

        if (roll < 60)
            return ItemTier.Tier1;

        if (roll < 85)
            return ItemTier.Tier2;

        if (roll < 95)
            return ItemTier.Tier3;

        return ItemTier.Tier4;
    }

    private Item GetRandomItem()
    {
        for (int i = 0; i < 10; i++)
        {
            ItemTier tier = RollTier();

            if (!tierItems.TryGetValue(tier, out List<Item> items))
                continue;

            if (items.Count == 0)
                continue;

            return items[Random.Range(0, items.Count)];
        }

        return null;
    }

    public void Reroll()
    {
        for (int i = 0; i < shopButtons.Length; i++)
        {
            Item item = GetRandomItem();
            if (item != null)
            {
                shopButtons[i].SetItem(item);
            }
        }
    }
}
