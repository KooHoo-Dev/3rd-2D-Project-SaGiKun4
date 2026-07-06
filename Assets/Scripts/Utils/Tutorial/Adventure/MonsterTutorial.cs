using UnityEngine;

public class TutorialMonster : MonoBehaviour
{
    private void OnDisable()
    {
        AdventureTutorial.Instance.MonsterComplete();
    }
}