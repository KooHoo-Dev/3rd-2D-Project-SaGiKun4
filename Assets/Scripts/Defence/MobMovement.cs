using UnityEditor.MPE;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    [SerializeField] private Transform[] mapPoint;
    private Vector3 goalPoint;
    private Vector3 currentPoint;

    private float speed = 1.0f;
    private int mapPointIndex = 0;
    

    private void Update()
    {
        Move();
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
}
