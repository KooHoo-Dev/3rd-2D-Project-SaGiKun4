using Jay;
using Study.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
}
