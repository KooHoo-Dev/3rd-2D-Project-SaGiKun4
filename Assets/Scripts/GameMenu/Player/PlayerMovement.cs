using Study.Utilities;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;

    private int moveInput = 0;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(SimpleInput.GetKey(Key.LeftArrow) || SimpleInput.GetKey(Key.A))
        {
            moveInput = -1;
        }

        if(SimpleInput.GetKey(Key.RightArrow) || SimpleInput.GetKey(Key.D))
        {
            moveInput = 1;
        }

        player.MovePosition(transform.position + new Vector3(moveInput, 0) * Time.fixedDeltaTime);

        moveInput = 0;
    }
}
