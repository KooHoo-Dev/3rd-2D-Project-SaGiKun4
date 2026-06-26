using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pod : MonoBehaviour
{
    [SerializeField] private GameObject bound;
    private bool inExitZone = false;

    private void Awake()
    {
        bound.SetActive(false);
    }

    private void Update()
    {
        if (inExitZone)
        {
            if(SimpleInput.GetKey(Key.Space))
            {
                SceneManager.LoadScene("GameMenu");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inExitZone = true;
            bound.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inExitZone = false;
            bound.SetActive(false);
        }
    }
}
