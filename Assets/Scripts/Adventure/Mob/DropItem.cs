using UnityEngine;

public class DropItem : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Item Item { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ItemInitialized(Item inputItem)
    {
        Item = inputItem;
        spriteRenderer.sprite = Item.Icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.Instance.TempInventory.AddItem(Item);
            gameObject.SetActive(false);
        }
    }
}
