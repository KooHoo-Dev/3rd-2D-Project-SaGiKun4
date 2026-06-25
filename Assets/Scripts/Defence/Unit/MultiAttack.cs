using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/MultiAttack")]
public class MultiAttack : UnitAttack
{
    public override void Attack(Unit unit, List<MobState> monsters)
    {
        if (monsters.Count == 0)
            return;

        for (int i = 0; i < monsters.Count; i++) 
        {
            Debug.Log(monsters[i].name + "를 공격");
            monsters[i].TakeDamage(unit.damage);
        }
    }
}