using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDefenceController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Vector2 DefaultPos;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        DefaultPos = transform.position; 
        GetComponent<Image>().raycastTarget = false; 
    }
	
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Vector2 currentPos = new Vector2(transform.position.x,transform.position.y) + eventData.delta;
        transform.position = currentPos;
    }
	
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        transform.position = DefaultPos;
        GetComponent<Image>().raycastTarget = true;
    }
}
