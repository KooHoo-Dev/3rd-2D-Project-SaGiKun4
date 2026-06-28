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
        item.Buy(spawner);
        ContentController.Instance.Refresh();
    }
}
