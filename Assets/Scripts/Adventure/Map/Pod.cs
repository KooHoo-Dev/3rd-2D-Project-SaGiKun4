using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pod : MonoBehaviour
{
    private bool inExitZone = false;

    private void Update()
    {
        if (inExitZone)
        {
            if(SimpleInput.GetKey(Key.Space))
            {
                AdventureManager.Instance.QuitAdventure();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inExitZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inExitZone = false;
        }
    }
}
