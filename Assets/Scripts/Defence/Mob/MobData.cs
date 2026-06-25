using UnityEngine;

[CreateAssetMenu(menuName = "Defence Monster Data")]
public class MonsterData : ScriptableObject
{
    public int maxHp;
    public float moveSpeed;
    public Material material;
    public RuntimeAnimatorController animatorController;
}