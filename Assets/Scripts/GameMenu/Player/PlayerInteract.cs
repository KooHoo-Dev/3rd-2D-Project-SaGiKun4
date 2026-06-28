using Study.Utilities;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private InteractionPoint currentPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            currentPoint = collision.GetComponent<InteractionPoint>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            currentPoint = null;
        }
    }

    private void Update()
    {
        if (currentPoint != null && SimpleInput.GetKey(Key.Space))
        {
            currentPoint.Execute();
        }
    }
}
