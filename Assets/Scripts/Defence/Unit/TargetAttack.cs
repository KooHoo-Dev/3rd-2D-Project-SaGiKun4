using System.Collections.Generic;
using UnityEngine;

public class TargetAttack : UnitAttack
{
    private int damage;
    private Unit unit;

    private void Start()
    {
        unit = GetComponentInParent<Unit>();
        damage = unit.damage;
    }

    public override void Attack(List<MobState> monsters)
    {
        if (monsters.Count == 0)
            return;

        Debug.Log(monsters[0].name+"를 공격");
        monsters[0].TakeDamage(damage);
    }
}