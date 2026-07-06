using System.Collections;
using TMPro;
using UnityEngine;
using static TutorialManager;

public class MenuTutorial : MonoBehaviour
{
    [SerializeField] private GameObject adventureArrow;
    [SerializeField] private GameObject adventureCollider;

    [SerializeField] private GameObject defenceArrow;

    [SerializeField] private GameObject defaultCollider;
    [SerializeField] private GameObject statusHighlight;
    [SerializeField] private GameObject trainHighlight;

    [SerializeField] private TMP_Text tutorialText;

    private bool reachedAdventure = false;
    private int forgeStep = -1;

    private void Start()
    {
        tutorialText.gameObject.SetActive(false);

        switch (TutorialManager.Instance.CurrentState)
        {
            case TutorialState.None:
                StartAdventureTutorial();
                break;

            case TutorialState.GameMenu_Defence:
                StartDefenceTutorial();
                break;

            case TutorialState.GameMenu_Forge:
                StartForgeTutorial();
                break;

            default:
                gameObject.SetActive(false);
                break;
        }
    }

    private void StartAdventureTutorial()
    {
        adventureArrow.SetActive(true);
        adventureCollider.SetActive(true);

        tutorialText.gameObject.SetActive(true);
        tutorialText.text = "탐험을 떠나기 위해 화살표 방향으로 이동해주세요.";
    }

    public void ReachAdventure()
    {
        if (reachedAdventure)
            return;

        reachedAdventure = true;

        TutorialManager.Instance.CurrentState = TutorialState.Adventure;

        tutorialText.text = "이동 완료!\n스페이스바를 눌러 탐험을 시작해볼까요?";
    }

    private void StartDefenceTutorial()
    {
        defenceArrow.SetActive(true);

        tutorialText.gameObject.SetActive(true);
        tutorialText.text = "야생동물이 둥지를 노리고 있어요!\n왼쪽의 둥지로 이동해주세요!";

        TutorialManager.Instance.CurrentState = TutorialState.Defence;
    }

    private void StartForgeTutorial()
    {
        forgeStep = 0;

        defaultCollider.SetActive(false);

        statusHighlight.SetActive(false);
        trainHighlight.SetActive(false);

        tutorialText.gameObject.SetActive(true);

        StartCoroutine(ForgeStartRoutine());
    }

    private IEnumerator ForgeStartRoutine()
    {
        tutorialText.text = "이제 마지막으로 강화를 설명하겠습니다.";

        yield return new WaitForSeconds(3f);

        statusHighlight.SetActive(true);

        tutorialText.text = "강화 옵션은 상태보기 버튼을 통해 들어갈 수 있습니다.";
    }

    public void StatusButtonClicked()
    {
        if (forgeStep != 0)
            return;

        forgeStep = 1;

        statusHighlight.SetActive(false);
        trainHighlight.SetActive(true);

        StartCoroutine(ForgeGuideRoutine());
    }

    private IEnumerator ForgeGuideRoutine()
    {
        tutorialText.text = "강화 옵션은 총 3가지로 강화를 원하는 부위를 선택하여 진행할 수 있습니다.";

        yield return new WaitForSeconds(3f);

        tutorialText.text = "원하는 부위를 강화해보세요!";
    }

    public void TrainCompleted()
    {
        if (forgeStep != 1)
            return;

        forgeStep = 2;

        trainHighlight.SetActive(false);

        tutorialText.text = "튜토리얼이 종료되었습니다.\n이제 자유롭게 모험을 떠나보세요!";
        defaultCollider.SetActive(true);
        TutorialManager.Instance.CurrentState = TutorialState.Finish;
    }
}