using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentController : MonoBehaviour
{
    public static ContentController Instance { get; private set; }

    [SerializeField] private Button[] rollItemButton;
    [SerializeField] private Button rerollButton;
    [SerializeField] private TMP_Text pointText;

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
            else 
            {
                slots[i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < rollItemButton.Length; i++)
        {
            rollItemButton[i].TryGetComponent<ItemButtonBehaviour>(out ItemButtonBehaviour rollItemContent);

            if (rollItemContent == null) return;

            if (rollItemContent.item.price > GameFlowManager.Instance.defencePoint)
            {
                rollItemButton[i].interactable = false;
            }
            else
            {
                rollItemButton[i].interactable = true;
            }
        }

        if (GameFlowManager.Instance.defencePoint < 1)
        {
            rerollButton.interactable = false;
        }
        else
        {
            rerollButton.interactable = true;
        }

        pointText.SetText(GameFlowManager.Instance.defencePoint.ToString());
    }
}
