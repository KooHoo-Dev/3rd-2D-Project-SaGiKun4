using Study.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private InteractionPoint currentPoint;
    [SerializeField] private TMP_Text text;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            currentPoint = collision.GetComponent<InteractionPoint>();
            text.SetText(currentPoint.InteractionText);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            currentPoint = null;
            text.SetText("");
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
