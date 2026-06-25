using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] slots;

    private Unit[] units;

    private void Awake()
    {
        units = new Unit[slots.Length];
    }

    public int FindEmptySlot()
    {
        for (int i = 0; i < units.Length; i++)
        {
            if (units[i] == null)
                return i;
        }

        return -1;
    }

    public void Spawn(Unit prefab)
    {
        int slotIndex = FindEmptySlot();

        if (slotIndex == -1)
            return;

        Unit unit = Instantiate(prefab,slots[slotIndex].position,Quaternion.identity);

        units[slotIndex] = unit;
    }
}
