using Study.Utilities;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class PlayerMenuMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    private InteractionPoint currentInteraction;
    [SerializeField] private TMP_Text text;

    private int moveInput = 0;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        text = GetComponentInChildren<TMP_Text>();
    }

    private void FixedUpdate()
    {
        player.MovePosition(transform.position + new Vector3(moveInput, 0) * Time.fixedDeltaTime);

        moveInput = 0;
    }

    private void Update()
    {
        if(SimpleInput.GetKey(Key.LeftArrow) || SimpleInput.GetKey(Key.A))
        {
            moveInput = -3;
        }

        if(SimpleInput.GetKey(Key.RightArrow) || SimpleInput.GetKey(Key.D))
        {
            moveInput = 3;
        }

        if (SimpleInput.GetKey(Key.Space))
        {
            if (currentInteraction != null)
            {
                currentInteraction.Execute();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            currentInteraction = collision.GetComponent<InteractionPoint>();
            text.SetText(currentInteraction.InteractionText);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Point"))
        {
            currentInteraction = null;
            text.SetText("");
        }
    }
}
