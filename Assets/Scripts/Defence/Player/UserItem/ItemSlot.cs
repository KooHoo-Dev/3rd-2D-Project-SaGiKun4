using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [field: SerializeField] public Image Content {get; private set;}

    [field: SerializeField] public Item item {get; private set;}
    [field: SerializeField] public TMP_Text nameTxt { get; private set; }
    [field: SerializeField] public TMP_Text countTxt {get; private set;}
    private int count;

    private void Start()
    {
        Content.sprite = item.Icon;
        nameTxt.text = item.Name;
        countTxt.text = InventoryManager.Instance.Inventory.GetItemCount(item).ToString();
        Content.enabled = true;
    }

    public bool HaveSlotItem()
    {
        if (InventoryManager.Instance.Inventory.GetItemCount(item) <= 0) return false;

        return true;
    }

    public void RefreshCount()
    {
        countTxt.text = InventoryManager.Instance.Inventory.GetItemCount(item).ToString();
    }
}
