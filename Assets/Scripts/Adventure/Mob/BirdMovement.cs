using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public enum State
    {
        None,
        Idle,
        Run,
        Attack,
        Return
    }

    public enum BirdType
    {
        Passive,
        Aggressive,
        Ignore
    }

    public BirdType myBirdType;

    private SpriteRenderer spriteRenderer;

    // A* 패스파인딩
    [SerializeField] private PathFinder pathFinder;
    [SerializeField] private GridManager gridManager;
    private List<Node> path;
    Node prevPlayerNode;
    private int currentIndex;

    // 스폰 & Idle 행동 반경
    [SerializeField] private Collider2D moveArea;

    // Idle 랜덤 위치 이동용
    private float coolTime = 10f;
    private float timer = 0f;
    private Vector2 targetPos;

    [SerializeField] private State currentState = State.Idle;

    public Transform Player { get; private set; }

    public Rigidbody2D Rigidbody2D { get; private set; }

    // 새 기본 옵션
    [SerializeField] private BirdData birdData;
    [SerializeField] private float myHp;
    private float myDefaultSpeed;
    private float myRunSpeed;
    private float reflectDistance = 5f;
    private Vector2 returnPos;
    private float returnAllowDistance = 1f;
    private BirdSkill currentSkill;
    private List<BirdSkill> skills;
    [SerializeField] private GameObject dropItem;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        skills = new List<BirdSkill>(GetComponents<BirdSkill>());
    }

    private void Start()
    {
        // 새 기본값 설정
        returnPos = Rigidbody2D.transform.position;
        myHp = birdData.hp;
        myDefaultSpeed = birdData.defaultSpeed;
        myRunSpeed = birdData.runSpeed;
        myBirdType = birdData.birdType;

        // Idle 랜덤 이동 쿨타임
        timer = coolTime;
    }

    private void Update()
    {
        if(myHp <= 0)
        {
            dropItem = Instantiate(dropItem);
            dropItem.transform.position = transform.position;
            DropItem dropitem1 = dropItem.AddComponent<DropItem>();
            dropitem1.ItemInitialized(birdData.dropItem);
            
            gameObject.SetActive(false);
        }

        OrderState();
        ExecuteState();
    }

    private void OrderState()
    {
        if (currentState == State.Return)
            return;

        if (!moveArea.bounds.Contains(transform.position))
        {
            ChangeState(State.Return);
            return;
        }

        if (currentSkill != null)
            return;

        if (currentState == State.None)
            return;

        if (Player != null && CanSeePlayer())
        {
            if (myBirdType == BirdType.Passive)
            {
                ChangeState(State.Run);
            }
            else if (myBirdType == BirdType.Aggressive)
            {
                ChangeState(State.Attack);
            }
            else
            {
                ChangeState(State.Idle);
            }

            return;
        }

        ChangeState(State.Idle);
    }

    private void ChangeState(State nextState)
    {
        if (currentState == nextState)
            return;

        if (currentSkill != null)
        {
            currentSkill.CancelSkill();
            currentSkill = null;
        }

        path = null;

        currentState = nextState;
    }

    private void ExecuteState()
    {
        switch (currentState)
        {
            case State.None:
                break;

            case State.Idle:
                Idle();
                break;

            case State.Run:
                Run();
                break;

            case State.Attack:
                Attack();
                break;

            case State.Return:
                Return();
                break;
        }
    }

    private bool CanSeePlayer()
    {
        Vector2 myPos = transform.position;
        Vector2 PlayerPos = Player.position;
        Vector2 dir = (PlayerPos - myPos);

        float distance = dir.magnitude;
        dir = dir.normalized;

        RaycastHit2D hit = Physics2D.Raycast(myPos, dir, distance, LayerMask.GetMask("Wall"));

        return hit.collider == null;
    }

    private void Move(float speed)
    {
        if (path == null)
            return;

        if (currentIndex < 0 || currentIndex >= path.Count)
            return;

        Vector2 pathNode = path[currentIndex].worldPosition;

        Vector2 dir = (pathNode - (Vector2)transform.position).normalized;

        FocusOnDirection(dir);

        Rigidbody2D.linearVelocity = dir * speed;

        if (Vector2.Distance(transform.position, pathNode) < 0.5f)
        {
            currentIndex++;

            if (currentIndex >= path.Count)
            {
                Rigidbody2D.linearVelocity = Vector2.zero;
            }
        }
    }

    public void FocusOnDirection(Vector2 dir)
    {
        transform.right = dir;

        spriteRenderer.flipY = dir.x < 0f && Mathf.Abs(dir.y) > 0.01f; // 회전시 z축으로 하다가 z = 180만 y = 180으로 변경해서 값 넣는거 보정용
    }

    private bool PlayerNodeChange()
    {
        Node currentNode = gridManager.NodePositionFromWorldPoint(Player.position);

        if (currentNode == prevPlayerNode)
            return false;

        prevPlayerNode = currentNode;
        return true;
    }

    private void SetPathToTarget(Vector2 target)
    {
        path = pathFinder.FindPath(transform.position, target);
        currentIndex = 0;
    }

    private void Attack()
    {
        if (currentSkill != null)
        {
            currentSkill.Execute(this);

            if (currentSkill.IsFinish)
            {
                currentSkill = null;
            }

            return;
        }

        foreach (var skill in skills)
        {
            if (skill.CanUse(this))
            {
                currentSkill = skill;
                currentSkill.CurrentSkillInit(this);
                return;
            }
        }

        Chase();
    }

    private void Chase()
    {
        if (PlayerNodeChange())
        {
            SetPathToTarget(Player.position);
        }

        Move(myRunSpeed);
    }

    private void Run()
    {
        if (path == null || currentIndex >= path.Count)
        {
            Vector2 runDir = ((Vector2)transform.position - (Vector2)Player.position).normalized;
            Vector2 runPosition = (Vector2)transform.position + runDir;

            Node targetNode = gridManager.NodePositionFromWorldPoint(runPosition);

            if (!targetNode.walkable)
            {
                runDir = ((Vector2)Player.position - (Vector2)transform.position).normalized;
                runPosition = (Vector2)transform.position + runDir * reflectDistance;
            }

            SetPathToTarget(runPosition);
        }

        Move(myRunSpeed);
    }

    private void Idle()
    {
        timer -= Time.deltaTime;

        if (path == null)
        {
            targetPos = PickRandomPosition();
            SetPathToTarget(targetPos);
            return;
        }

        if (currentIndex >= path.Count)
        {
            return;
        }

        Move(myDefaultSpeed);

        if (timer <= 0f)
        {
            targetPos = PickRandomPosition();
            SetPathToTarget(targetPos);

            timer = coolTime;
        }
    }

    private void Return()
    {
        if (path == null || currentIndex >= path.Count)
        {
            SetPathToTarget(returnPos);
        }

        Move(myRunSpeed);

        if (Vector2.Distance(transform.position, returnPos) < returnAllowDistance)
        {
            ChangeState(State.Idle);
        }
    }

    private Vector2 PickRandomPosition()
    {
        Bounds bounds = moveArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }

    public void OnPlayerDetected(Transform target)
    {
        Player = target;
    }

    public void OnPlayerLost()
    {
        Player = null;
    }

    public void TakeDamage(int damage)
    {
        myHp -= damage;
    }
}
