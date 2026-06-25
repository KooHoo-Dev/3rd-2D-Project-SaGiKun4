using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float accel = 5f;

    private Rigidbody2D rigid;

    // New InputSystem
    private PlayerInput inputActions;
    private Vector2 input;

    // 공격 기능용 방향
    private float lookDir = 1;
    private Vector3 originalScale;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

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
            Debug.Log("어택");
        }
    }
}
