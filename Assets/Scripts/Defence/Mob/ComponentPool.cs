using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> where T : Component
{
    private readonly T original;
    private readonly Transform parent;

    private readonly List<T> spawned = new List<T>();

    public ComponentPool(T original, Transform parent = null)
    {
        this.original = original;
        this.parent = parent;
    }

    public T Get()
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            if (spawned[i].gameObject.activeSelf == false)
            {
                spawned[i].gameObject.SetActive(true);
                return spawned[i];
            }
        }

        T newOne = Object.Instantiate(original, parent);
        spawned.Add(newOne);
        newOne.gameObject.SetActive(true);
        return newOne;
    }
}

