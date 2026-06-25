using Jay;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : SingletonBase<UnlockManager>
{
    private HashSet<Item> unlockedItems = new();

    [SerializeField] public Item[] items;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        for (int i = 0; i < items.Length; i++)
        {
            Unlock(items[i]);
        }
    }

    public bool IsUnlocked(Item item)
    {
        return unlockedItems.Contains(item);
    }

    public void Unlock(Item item)
    {
        unlockedItems.Add(item);
    }
}
