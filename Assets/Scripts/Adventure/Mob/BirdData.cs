using UnityEngine;
using static BirdMovement;

[CreateAssetMenu]
public class BirdData : ScriptableObject
{
    public int hp;
    public int damage;
    public float defaultSpeed;
    public float runSpeed;
    public BirdType birdType;
}
