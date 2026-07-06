using TMPro;
using UnityEngine;

public class AdventureTutorial : MonoBehaviour
{
    public static AdventureTutorial Instance { get; private set; }

    [SerializeField] private TMP_Text tutorialText;

    [SerializeField] private GameObject moveCollider;
    [SerializeField] private GameObject dashCollider;
    [SerializeField] private GameObject tutorialMonster;
    [SerializeField] private GameObject returnCollider;
    [SerializeField] private GameObject tutorialCollider;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (TutorialManager.Instance.CurrentState != TutorialManager.TutorialState.Adventure)
        {
           gameObject.SetActive(false);
           return;
        }

        moveCollider.SetActive(true);
        tutorialCollider.SetActive(true);
        dashCollider.SetActive(false);
        tutorialMonster.SetActive(false);
        returnCollider.SetActive(false);

        tutorialText.text = "WASD 또는 방향키를 이용하여 \n화살표까지 이동하세요.";
    }

    public void MoveComplete()
    {
        moveCollider.SetActive(false);
        dashCollider.SetActive(true);

        tutorialText.text = "더 멀리 이동해볼까요? \nShift키를 눌러 더 빠르게 이동해보세요!";
    }

    public void DashComplete()
    {
        dashCollider.SetActive(false);
        tutorialMonster.SetActive(true);

        tutorialText.text = "좌클릭을 통해 근접 공격을 하거나 \n우클릭으로 조준하여 원거리 공격을 통해 \n참새를 처치하고 아이템을 획득하세요!";
    }

    public void MonsterComplete()
    {
        tutorialMonster.SetActive(false);
        returnCollider.SetActive(true);
        TutorialManager.Instance.CurrentState = TutorialManager.TutorialState.GameMenu_Defence;
        tutorialText.text = "잘하셨습니다! \n 이제 맵 최하단으로 이동하여 숲으로 귀환하세요!";
    }
}