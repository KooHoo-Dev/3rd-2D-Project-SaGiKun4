using UnityEngine;

public class MobState : MonoBehaviour
{
    [SerializeField] private Transform[] mapPoint;
    private Vector3 goalPoint;
    private Vector3 currentPoint;
    private int mapPointIndex = 0;

    private float speed = 1.0f;
    public int currentHp;

    public void OnEnabled()
    {
        currentHp = 10;
    }

    private void Update()
    {
        if(currentHp >= 0)
            Move();
        else
            Dead();
    }

    private void Move()
    {
        currentPoint = Vector3.MoveTowards(transform.position, goalPoint, Time.deltaTime * speed);
        transform.position = currentPoint;

        if(currentPoint == goalPoint)
        {
            goalPoint = mapPoint[mapPointIndex].position;
            mapPointIndex++;

            if(mapPointIndex >= mapPoint.Length) mapPointIndex = 0;
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        //그냥 애니메이터만 바꿔주면 될듯
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }
}
