using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/TargetAttack")]
public class TargetAttack : UnitAttack
{
    public override void Attack(Unit unit, List<MobState> monsters)
    {
        if (monsters.Count == 0)
            return;

        monsters[0].TakeDamage(unit.damage);
    }
}