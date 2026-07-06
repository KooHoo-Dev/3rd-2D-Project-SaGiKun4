using UnityEngine;

public class MenuTutorialCollider : MonoBehaviour
{
    [SerializeField] private MenuTutorial menuTutorial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        menuTutorial.ReachAdventure();
    }
}
