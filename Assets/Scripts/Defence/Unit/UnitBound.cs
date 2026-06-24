using System.Collections.Generic;
using UnityEngine;

public class UnitBound : MonoBehaviour
{
    private UnitBehaviour unit;

    private void Awake()
    {
        unit = GetComponentInParent<UnitBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob"))
        {
            MobState mob = collision.GetComponent<MobState>();
            unit.mobs.Add(mob);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob"))
        {
            MobState mob = collision.GetComponent<MobState>();
            unit.mobs.Remove(mob);
        }
    }
}
