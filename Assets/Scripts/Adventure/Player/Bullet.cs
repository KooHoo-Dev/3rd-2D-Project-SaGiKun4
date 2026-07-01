using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletOwner
    {
        Player,
        Monster
    }

    [SerializeField] private float speed = 10f;

    private BulletOwner owner;
    private SpriteRenderer spriteRenderer;
    private Vector2 direction;
    [SerializeField] private LayerMask wallLayer;
    private int damage;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(BulletOwner owner, Sprite sprite, int damage, Vector2 direction, float rotationOffset)
    {
        this.owner = owner;
        this.damage = damage;
        this.direction = direction.normalized;

        spriteRenderer.sprite = sprite;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob") && owner == BulletOwner.Player)
        {
            BirdMovement bird = collision.GetComponent<BirdMovement>();
            bird.TakeDamage(damage);
            gameObject.SetActive(false);
        }

        if (collision.CompareTag("Player") && owner == BulletOwner.Monster)
        {
            PlayerBehaviour player = collision.GetComponent<PlayerBehaviour>();
            player.TakeDamage(damage);
            gameObject.SetActive(false);
        }

        if(collision.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}