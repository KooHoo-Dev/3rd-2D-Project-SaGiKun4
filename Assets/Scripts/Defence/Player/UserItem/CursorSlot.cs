using UnityEngine;
using UnityEngine.UI;
public class CursorSlot : MonoBehaviour
{
    [field: SerializeField]public Image Content {get; private set;}
    private Item item;

    private RectTransform rectTransform;

    public void SetItem(Item inputItem)
    {
        item = inputItem;

        if(item == null)
        {
            Content.sprite = null;
            Content.enabled = false;
        } else
        {
            Content.sprite = item.Icon;
            Content.enabled = true;
        }
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetPosition(Vector2 inputPosition)
    {
        rectTransform.anchoredPosition = inputPosition;
    }
}
