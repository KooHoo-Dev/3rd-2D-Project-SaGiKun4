using Study.Utilities;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class PlayerMenuMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    private InteractionPoint currentInteraction;
    

    private int moveInput = 0;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
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
    }
}
