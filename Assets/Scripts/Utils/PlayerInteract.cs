using Study.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private InteractionPoint currentPoint;
    [SerializeField]private GameObject spaceKey;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            currentPoint = collision.GetComponent<InteractionPoint>();
            spaceKey.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            currentPoint = null;
            spaceKey.SetActive(false);
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
