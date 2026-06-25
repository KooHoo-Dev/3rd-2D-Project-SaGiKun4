using System.Collections.Generic;
using UnityEngine;

public class DetectedArea : MonoBehaviour
{
    private Unit unit;

    private void OnEnable()
    {
        unit = GetComponentInParent<Unit>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            unit.mobs.Add(other.GetComponent<MobState>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            unit.mobs.Remove(other.GetComponent<MobState>());
        }
    }
}