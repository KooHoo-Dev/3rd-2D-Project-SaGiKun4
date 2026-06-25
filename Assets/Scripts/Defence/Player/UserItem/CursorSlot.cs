using UnityEngine;
using UnityEngine.UI;
public class CursorSlot : MonoBehaviour
{
    [field: SerializeField]public Image Content {get; private set;}
    private Item item;

    private RectTransform rectTransform;

    /// <summary>
    /// Null을 허용하는 함수입니다. Null 전달 시 Slot이 비워집니다.
    /// </summary>
    /// <param name="inputItem"></param>
    public void SetItem(Item inputItem)
    {
        // inputItem은 Null이 들어오거나, 실제 InventoryItem 객체가 들어옵니다
        item = inputItem;

        if(item == null) // item이 없는 경우
        {
            Content.sprite = null; // 이미지의 sprite를 비워준다.
            Content.enabled = false; // 이미지의 활성화를 off한다.
        } else // item이 있는 경우
        {
            Content.sprite = item.Icon; // 이미지의 sprite를 item.Icon으로 설정.
            Content.enabled = true; // 이미지의 활성화를 on 해준다.
        }
    }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // 외부에서 위치값을 입력 받아서 Slot의 위치를 갱신합니다.
    // Cursor 슬롯 전용 함수입니다.
    public void SetPosition(Vector2 inputPosition)
    {
        rectTransform.anchoredPosition = inputPosition;
    }
}
