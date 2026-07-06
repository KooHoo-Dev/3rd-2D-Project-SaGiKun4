using UnityEngine;

public class DashTutorial : MonoBehaviour
{
    [SerializeField] private AdventureTutorial adventureTutorial;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        adventureTutorial.DashComplete();
    }
}
