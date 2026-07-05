using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/MoneyMaker")]
public class MoneyMaker: UnitAttack
{
    public override void Attack(Unit unit, List<MobState> monsters)
    {
        GameFlowManager.Instance.defencePoint++;
        ContentController.Instance.Refresh();
    }
}