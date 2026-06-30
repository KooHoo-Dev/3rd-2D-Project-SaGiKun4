using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonBehaviour : MonoBehaviour
{
    [SerializeField] private UnitSpawner spawner;
    [SerializeField] private Item item;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemCost;

    public void Start()
    {
        SetItem(item);
    }

    public void SetItem(Item item)
    {
        this.item = item;
        image.sprite = item.Icon;
        itemName.text = item.Name;
        itemCost.text = $"{item.price}";
    }

    public void OnClick()
    {
        if(item.CanBuy())
        {
            item.Buy();
            gameObject.SetActive(false);
        }
        ContentController.Instance.Refresh();
    }
}
