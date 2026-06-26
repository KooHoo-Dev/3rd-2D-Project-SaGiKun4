using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float accel = 5f;
    private Animator animator;

    private Rigidbody2D rigid;

    [SerializeField] public List<BirdMovement> mobs = new();

    // New InputSystem
    private PlayerInput inputActions;
    private Vector2 input;

    // 공격 기능용 방향
    private float lookDir = 1;
    private Vector3 originalScale;

    public int damage = 1;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        inputActions = new PlayerInput();

        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.PlayerMove.Attack.performed += OnAttack;
        //inputActions.PlayerMove.Aim.started += OnAimStarted;
        //inputActions.PlayerMove.Aim.canceled += OnAimCanceled;
    }

    private void OnDisable()
    {
        inputActions.PlayerMove.Attack.performed -= OnAttack;
        //inputActions.PlayerMove.Aim.started -= OnAimStarted;
        //inputActions.PlayerMove.Aim.canceled -= OnAimCanceled;

        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 targetVel = input * moveSpeed;

        rigid.linearVelocity = Vector2.Lerp(rigid.linearVelocity, targetVel, accel * Time.fixedDeltaTime);
    }

    private void Update()
    {
        input = inputActions.PlayerMove.Move.ReadValue<Vector2>();

        if (input.x != 0)
        {
            lookDir = Mathf.Sign(input.x);
        }
        
        transform.localScale = new Vector3(originalScale.x * lookDir, originalScale.y, originalScale.z);
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Attack");
        }
    }
}