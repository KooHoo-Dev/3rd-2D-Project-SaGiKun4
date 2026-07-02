using UnityEngine;

public class AnimatorAttack : MonoBehaviour
{
    private Unit parent;
    private void Start()
    {
        parent = GetComponentInParent<Unit>();
    }

    private void OnAttack() 
    {
        parent.Attack();
    }
}
