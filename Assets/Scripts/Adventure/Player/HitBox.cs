using UnityEngine;
using System.Collections.Generic;
public class HitBox : MonoBehaviour
{
    private PlayerBehaviour player;
    private HashSet<BirdMovement> checkList = new HashSet<BirdMovement>();

    private void Start()
    {
        player = GetComponentInParent<PlayerBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<BirdMovement>(out BirdMovement bird))
        {
            if(bird == null) return;
            if (checkList.Contains(bird)) return;

            checkList.Add(bird);

            bird.TakeDamage(PlayerBehaviour.Instance.damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        checkList.Clear();
    }
}
