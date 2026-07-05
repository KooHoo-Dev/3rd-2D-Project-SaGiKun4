using Jay;
using System.Collections.Generic;
using UnityEngine;

public class UnlockManager : SingletonBase<UnlockManager>
{
    [SerializeField] private HashSet<Item> unlockedItems = new();

    public bool IsUnlocked(Item item)
    {
        return unlockedItems.Contains(item);
    }

    public void Unlock(Item item)
    {
        unlockedItems.Add(item);
    }

    public int GetUnlockedItemCount()
    {
        return unlockedItems.Count;
    }
}
