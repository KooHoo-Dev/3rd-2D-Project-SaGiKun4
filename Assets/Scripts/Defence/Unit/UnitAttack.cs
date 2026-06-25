using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAttack : ScriptableObject
{
    public abstract void Attack(Unit unit, List<MobState> mobs);
}
