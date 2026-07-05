using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private Dictionary<Item, int> items = new();

    public void AddItem(Item item, int count = 1)
    {
        if (item == null) return;

        if (items.ContainsKey(item))
            items[item] += count;
        else
            items.Add(item, count);
    }

    public bool RemoveItem(Item item, int count = 1)
    {
        if (!items.TryGetValue(item, out int current))
            return false;

        if (current < count)
            return false;

        current -= count;

        if (current == 0)
            items.Remove(item);
        else
            items[item] = current;

        return true;
    }

    public void Merge(Inventory other)
    {
        foreach (KeyValuePair<Item, int> pair in other.items)
        {
            AddItem(pair.Key, pair.Value);
            UnlockManager.Instance.Unlock(pair.Key);
        }
    }

    public void Clear()
    {
        items.Clear();
    }

    public int GetItemCount(Item item)
    {
        if (items.TryGetValue(item, out int count))
        {
            return count;
        }

        return 0;
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }
}