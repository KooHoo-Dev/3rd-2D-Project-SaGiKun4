using System.Transactions;
using Study.Utilities;
using UnityEngine;

public class UserInputManager : MonoBehaviour
{
    [SerializeField] private LayerMask unitPositionMask;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            MouseInput();
        }
    }

    private void MouseInput()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit; 
        hit = Physics2D.Raycast(mousePosition, Vector3.forward, 100f, unitPositionMask);
        
        if(hit.collider != null)
            UnitSpawner.Instance.SelectUnit(hit.transform.GetComponentInChildren<Unit>());
        else
            UnitSpawner.Instance.DeSelectUnit();
    }
}
