using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [field: SerializeField] public Image Content {get; private set;}

    [field: SerializeField] public Item item {get; private set;}
    [field: SerializeField] public TMP_Text countTxt {get; private set;}
    private int count;

    private void Start()
    {
        Content.sprite = item.Icon;
        countTxt.SetText($"{item.Count}");
        Content.enabled = true;
    }
}
