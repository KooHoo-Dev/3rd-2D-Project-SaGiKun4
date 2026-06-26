using UnityEngine;

public class DropItem : MonoBehaviour
{
    private BoxCollider2D col2D;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        col2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ItemInitialized(Item item)
    {
        Debug.Log(item.Icon);
        spriteRenderer.sprite = item.Icon;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("플레이어 아이템 얻기");
        }
    }
}
