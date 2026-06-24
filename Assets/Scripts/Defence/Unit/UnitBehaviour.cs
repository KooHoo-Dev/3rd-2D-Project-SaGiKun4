using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private Collider2D bound;
    private UnitAttack attack;
    public List<MobState> mobs = new List<MobState>();

    public int damage {get; private set;} = 1;
    
    public float attackSpeed {get; private set;} = 1.0f;
    public float waitTime {get; private set;} = 2.0f;

    private void Update()
    {
        if(mobs.Count > 0)
        {
            attack.Attack(mobs);
        }
    }

    private void Awake()
    {
        attack = GetComponent<UnitAttack>();
    }


}
