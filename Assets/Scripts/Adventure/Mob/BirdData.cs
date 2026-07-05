using UnityEngine;
using static BirdMovement;

[CreateAssetMenu]
public class BirdData : ScriptableObject
{
    public int hp;
    public int damage;
    public string birdName;
    public float defaultSpeed;
    public float runSpeed;
    public BirdType birdType;
    public Item dropItem;
}
