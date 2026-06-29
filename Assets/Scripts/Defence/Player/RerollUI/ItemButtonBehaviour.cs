using UnityEngine;

public class ItemButtonBehaviour : MonoBehaviour
{
    [SerializeField] private UnitSpawner spawner;
    [SerializeField] private Item item;

    public void SetItem(Item item)
    {
        this.item = item;
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
