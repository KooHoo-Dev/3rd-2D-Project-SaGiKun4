using UnityEngine;
using static BirdMovement;

[CreateAssetMenu]
public class BirdData : ScriptableObject
{
    public int hp;
    public float defaultSpeed;
    public float runSpeed;
    public BirdType birdType;
}
