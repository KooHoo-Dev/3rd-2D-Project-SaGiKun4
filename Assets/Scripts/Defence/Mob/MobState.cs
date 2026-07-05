using UnityEngine;

public class MobState : MonoBehaviour
{
    private readonly int startPoint = 0;
    private enum RoadPointType
    {
        LeftBottom = 5,
        RightTop = 8
    }

    private Animator animator;
    private MeshRenderer meshRenderer;
    private Transform spriteFlipper;
    private Transform hpBar;

    [SerializeField] private Transform[] mapPoint;
    [SerializeField] private Vector3 goalPoint;
    [SerializeField] private Vector3 currentPoint;
    private int mapPointIndex = 0;

    private float speed;
    private int currentHp;
    private int maxHp;
    public bool isDead = false;

    public void OnEnable()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        spriteFlipper = GetComponentInChildren<Transform>();
        animator = GetComponentInChildren<Animator>();
        hpBar = GetComponentInChildren<SpriteMask>().transform;
    }

    public void InitializeStat(MonsterData data, Transform[] roadPoints, Vector2 spawnPosition)
    {
        isDead = false;
        maxHp = data.maxHp;
        currentHp = maxHp;
        speed = data.moveSpeed;
        meshRenderer.material = data.material;
        animator.runtimeAnimatorController = data.animatorController;
        transform.position = spawnPosition;
        spriteFlipper.localScale = Vector3.one;
        hpBar.localScale = new Vector3(0.95f, 1f, 1f);
        mapPointIndex = 0;

        mapPoint = roadPoints;
        goalPoint = roadPoints[startPoint].position;
    }

    private void Update()
    {
        if (isDead)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsTag("Death") && stateInfo.normalizedTime >= 1f)
            {
                GameFlowManager.Instance.UnregisterMonster(this);
                gameObject.SetActive(false);
            }

            return; // 죽었을때 Move 못들어가게
        }

        if(currentHp <= 0)
        {
            Dead();
        }
        else
        {
            Move();
        }
            
    }

    private void Move()
    {
        currentPoint = Vector3.MoveTowards(transform.position, goalPoint, Time.deltaTime * speed);
        transform.position = currentPoint;

        if(currentPoint == goalPoint)
        {
            // 뒤집기
            if (mapPointIndex == (int)RoadPointType.LeftBottom || mapPointIndex == (int)RoadPointType.RightTop)
            {
                spriteFlipper.localScale = new Vector3(-1 * spriteFlipper.localScale.x, spriteFlipper.localScale.y, spriteFlipper.localScale.z);
            }

            goalPoint = mapPoint[mapPointIndex].position;
            mapPointIndex++;

            if(mapPointIndex >= mapPoint.Length) mapPointIndex = startPoint;
        }
    }

    private void Dead()
    {
        isDead = true;
        GameFlowManager.Instance.defencePoint++;
        ContentController.Instance.Refresh();
        animator.SetTrigger("Death");
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        UpdateHPBar();
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
    }

    public void UpdateHPBar()
    {
        float ratio = (float)currentHp / maxHp;

        hpBar.localScale = new Vector3(0.95f * ratio, transform.localScale.y, transform.localScale.z);
    }
}
