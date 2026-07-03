using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [field:SerializeField] public CursorSlot CursorSlot {get; private set;}
    private Item selectedItem;
    private GraphicRaycaster raycaster;
    [SerializeField] private ScrollRect scrollRect;

    private void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        scrollRect  = GetComponent<ScrollRect>();
    }

    private Item GetItemOrNull(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        
        raycaster.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent(out ItemSlot item))
            {
                return item.item;
            }
        }

        return null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 시작 슬롯을 검색한다
        selectedItem = GetItemOrNull(eventData);
        if(selectedItem == null) return;

        scrollRect.enabled = false;
        CursorSlot.SetItem(selectedItem);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (selectedItem == null) return;
        
        CursorSlot.SetPosition(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (selectedItem == null) return;

        scrollRect.enabled = true;

        // 3. 드래그가 종료되면 상황에 따라 알맞은 처리를 한다
        //  3.1 종료된 위치의 Slot 자체가 없을 경우
        //  3.2 종료된 위치의 Slot이 Empty일 경우
        //  3.3 종료된 위치의 Slot에 아이템이 있을 경우

        Vector2 eventPos = Camera.main.ScreenToWorldPoint(eventData.position);
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(eventPos, Vector2.zero);
        
        if (hits != null)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.CompareTag("UnitMask"))
                {
                    Unit hitUnit = hit.collider.GetComponentInChildren<Unit>();
                    hitUnit.EquipItem(selectedItem);
                    ContentController.Instance.Refresh();
                    Debug.Log("유닛 실행");
                }
            }
        }

        ResetCursor();
    }

    private void ResetCursor()
    {
        CursorSlot.SetItem(null);
    }
}
