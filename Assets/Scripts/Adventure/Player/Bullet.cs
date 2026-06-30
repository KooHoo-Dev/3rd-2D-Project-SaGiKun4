using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Vector2 direction;
    private int damage;

    public void Fire(Vector2 direction, int damage)
    {
        this.direction = direction.normalized;
        this.damage = damage;
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob"))
        {
            Debug.Log("몹 맞음");
            gameObject.SetActive(false);
        }
    }
}