using UnityEngine;

public class MoveTutorial : MonoBehaviour
{
    [SerializeField] private AdventureTutorial adventureTutorial;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        adventureTutorial.MoveComplete();
    }
}
