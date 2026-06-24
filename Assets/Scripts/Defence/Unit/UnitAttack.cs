using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAttack : MonoBehaviour
{
    public abstract void Attack(List<MobState> mobs);
}
