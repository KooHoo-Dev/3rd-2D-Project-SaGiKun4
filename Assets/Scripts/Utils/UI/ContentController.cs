using UnityEngine;

public class ContentController : MonoBehaviour
{
    public static ContentController Instance { get; private set; }

    private ItemSlot[] slots;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        slots = GetComponentsInChildren<ItemSlot>();
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RefreshCount();
            if (!slots[i].HaveSlotItem())
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }
}
