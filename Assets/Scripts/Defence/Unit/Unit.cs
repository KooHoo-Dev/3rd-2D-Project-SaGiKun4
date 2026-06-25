using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private DetectedArea detectedArea;
    [SerializeField] private UnitAttack attackBehaviour;
    public List<MobState> mobs {get; private set;} = new();
    private Transform spriteFlipper;

    private float attackTimer;
    public int damage {get; private set;} = 10;

    private void OnEnable()
    {
        spriteFlipper = GetComponent<Transform>();
        attackBehaviour = GetComponent<UnitAttack>();
        attackTimer = attackSpeed;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackSpeed)
        {
            attackTimer = 0;

            attackBehaviour.Attack(mobs);
        }

        if(mobs.Count > 0)
        {
            Vector3 targetPos = mobs[0].transform.position;
            float sign = Mathf.Sign(targetPos.x - transform.position.x);

            sign = sign == Mathf.Sign(spriteFlipper.localScale.x) ? 1 : -1;

            spriteFlipper.localScale = new Vector3(sign * spriteFlipper.localScale.x, spriteFlipper.localScale.y, spriteFlipper.localScale.z);
        }
    }
}