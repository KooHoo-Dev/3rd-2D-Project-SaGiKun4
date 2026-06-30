using Study.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float accel = 5f;
    [SerializeField] private Animator childAnimator;
    [SerializeField] private LayerMask cameraMaskLayer;
    [SerializeField] private SimpleFollowCamera followCamera;

    private Rigidbody2D rigid;

    [SerializeField] public ParticleSystem hitEffect;

    // New InputSystem
    private PlayerInput inputActions;
    private Vector2 input;

    // 공격 기능용 방향
    private float lookDir = 1;
    private Vector3 originalScale;
    private bool canDamaged = true;

    [SerializeField] private Sprite playerBulletSprite;
    [SerializeField] private float rotationOffset;
    private bool isAiming = false;
    private Vector2 aimingOffset;

    private float bulletReloadingTime = 1.0f;
    private bool isReload = true;
    public int damage {get; private set;} = 3;
    public int currentHp {get; private set;} = 90;
    public int maxHp {get; private set;}

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        inputActions = new PlayerInput();

        originalScale = transform.localScale;
        
        InitializeStat();

        PlayerUIController.Instance.UpdateHealthUI();
    }

    private void InitializeStat()
    {
        canDamaged = true;

        damage = damage + (7 * PlayerDataManager.Instance.Power);
        maxHp = currentHp + (25  * PlayerDataManager.Instance.Health);
        currentHp = maxHp;
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.PlayerMove.Attack.performed += OnAttack;
        inputActions.PlayerMove.Aim.started += OnAimStarted;
        inputActions.PlayerMove.Aim.canceled += OnAimCanceled;
    }

    private void OnDisable()
    {
        inputActions.PlayerMove.Attack.performed -= OnAttack;
        inputActions.PlayerMove.Aim.started -= OnAimStarted;
        inputActions.PlayerMove.Aim.canceled -= OnAimCanceled;

        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        if (isAiming)
        {
            rigid.linearVelocity = Vector2.zero;
            return;
        }

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

        if (isAiming)
        {
            OnAiming();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isAiming && isReload)
            {
                StartCoroutine(Shoot());
            }
            else if(!isAiming)
            {
                childAnimator.SetTrigger("Attack");
            }
        }
    }

    private IEnumerator Shoot()
    {
        isReload = false;
        Bullet bullet = BulletPool.Instance.Get();

        bullet.transform.position = transform.position;
        bullet.Initialize(Bullet.BulletOwner.Player, playerBulletSprite, damage, aimingOffset.normalized, rotationOffset);

        yield return new WaitForSeconds(bulletReloadingTime);
        isReload = true;
    }

    public void OnAimStarted(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAiming = true;
        }
    }

    public void OnAiming()
    {
        Vector3 screenPos = Input.mousePosition;

        screenPos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(screenPos);

        Vector2 offset = (Vector2)(mouseWorldPos - transform.position);

        aimingOffset = offset.normalized;

        transform.localScale = new Vector3(originalScale.x * Mathf.Sign(offset.x), originalScale.y, originalScale.z);

        followCamera.SetOffset(offset);
        followCamera.lerpSpeed = 3;
    }

    public void OnAimCanceled(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            isAiming = false;
            followCamera.SetOffset(Vector2.zero);
            followCamera.lerpSpeed = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if (collision.TryGetComponent(out DropItem dropItem))
            {
                UnlockManager.Instance.Unlock(dropItem.Item);
            }
        }   
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob"))
        {
            if (collision.TryGetComponent(out BirdMovement bird))
            {
                if(bird.myBirdType == BirdMovement.BirdType.Aggressive)
                    StartCoroutine(DamageProcess(bird.myDamage));
            }
        }
    }

    private IEnumerator DamageProcess(int damage)
    {
        if (canDamaged)
        {
            canDamaged = false;
            currentHp -= damage;
            IsDead();
            PlayerUIController.Instance.UpdateHealthUI();
            hitEffect.gameObject.SetActive(false);
            hitEffect.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            canDamaged = true;
        }
    }

    private void IsDead()
    {
        if(currentHp <= 0)
        {
            AdventureManager.Instance.FailAdventure();
        }
    }
}