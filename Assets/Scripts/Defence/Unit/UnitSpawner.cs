using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public static UnitSpawner Instance { get; private set; }
    [SerializeField] private Transform[] slots;

    private Unit[] units;
    private Unit currentUnit = null;
    private SpriteRenderer detectedArea;

    private void Awake()
    {
        Instance = this;
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

        Unit unit = Instantiate(prefab, slots[slotIndex].transform);

        units[slotIndex] = unit;
    }

    public void SelectUnit(Unit unit)
    {
        if(unit == null) return;

        if(currentUnit != null) DeSelectUnit();
        
        currentUnit = unit;
        detectedArea = currentUnit.GetComponentInChildren<SpriteRenderer>();
        detectedArea.enabled = true;
    }

    public void DeSelectUnit()
    {
        if(currentUnit == null) return;
        currentUnit = null;
        detectedArea.enabled = false;
    }
}
