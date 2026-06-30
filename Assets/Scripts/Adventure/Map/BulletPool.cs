using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [SerializeField] private Bullet bulletPrefab;

    private ComponentPool<Bullet> pool;

    private void Awake()
    {
        Instance = this;
        pool = new ComponentPool<Bullet>(bulletPrefab, transform);
    }

    public Bullet Get()
    {
        return pool.Get();
    }
}