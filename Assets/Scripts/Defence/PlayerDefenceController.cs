using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDefenceController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Vector2 DefaultPos;
    private Image image;
    [SerializeField] private Image cursor;

    private void Start()
    {
        cursor.gameObject.SetActive(false);
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        cursor = GetComponent<Image>();
        DefaultPos = transform.position;
        cursor.gameObject.SetActive(true);
        GetComponent<Image>().raycastTarget = false; 
    }
	
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Vector2 currentPos = new Vector2(transform.position.x,transform.position.y) + eventData.delta;
        cursor.transform.position = currentPos;
    }
	
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Vector2 eventPos = Camera.main.ScreenToWorldPoint(eventData.position);
        RaycastHit2D hit = Physics2D.Raycast(eventPos, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Unit"))
            {
                // 유닛쪽 실행 코드
                Debug.Log("유닛 실행");
            }
        }

        transform.position = DefaultPos;
        cursor.gameObject.SetActive(false);
        GetComponent<Image>().raycastTarget = true;
    }
}
