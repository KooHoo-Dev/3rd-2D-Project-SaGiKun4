using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttack : UnitAttack
{
    private UnitBehaviour unit;

    private void Awake()
    {
        unit = GetComponent<UnitBehaviour>();
    }
    public override void Attack(List<MobState> mobs)
    {
        mobs[0].TakeDamage(unit.damage);
        Debug.Log("데미지");
    }
}
