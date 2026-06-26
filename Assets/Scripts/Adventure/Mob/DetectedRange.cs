using UnityEngine;

public class DetectedRange : MonoBehaviour
{
    BirdMovement birdMovement;

    private void Awake()
    {
        birdMovement = GetComponentInParent<BirdMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            birdMovement.OnPlayerDetected(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            birdMovement.OnPlayerLost();
        }
    }
}
