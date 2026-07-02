using Study.Utilities;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

public class PlayerMenuMovement : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator animator;
    

    private int moveInput = 0;
    private float dir = 1;
    private Vector3 defaultScale;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        defaultScale = animator.transform.localScale;
    }

    private void FixedUpdate()
    {
        if(moveInput != 0)
        {
            dir = Mathf.Sign(moveInput);
        }

        player.MovePosition(transform.position + new Vector3(moveInput, 0) * Time.fixedDeltaTime);

        animator.transform.localScale = new Vector3(defaultScale.x * dir, defaultScale.y, defaultScale.z);

        moveInput = 0;
    }

    private void Update()
    {
        animator.SetBool("isWalk", false);

        if(SimpleInput.GetKey(Key.LeftArrow) || SimpleInput.GetKey(Key.A))
        {
            moveInput = -3;
            animator.SetBool("isWalk", true);
        }

        if(SimpleInput.GetKey(Key.RightArrow) || SimpleInput.GetKey(Key.D))
        {
            moveInput = 3;
            animator.SetBool("isWalk", true);
        }
    }
}
