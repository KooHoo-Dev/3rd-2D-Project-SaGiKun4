using UnityEngine;

public class Slot : MonoBehaviour
{
    private enum Tier
    {
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5,
    }

    [SerializeField] private Item item;
}
