using System.Collections;
using TMPro;
using UnityEngine;

public class DefenceTutorial : MonoBehaviour
{
    [SerializeField] private GameObject shopHighlight;
    [SerializeField] private GameObject unitHighlight;
    [SerializeField] private GameObject inventoryHighlight;

    [SerializeField] private TMP_Text tutorialText;

    private int step = -1;
    private bool unitSelected = false;

    private void Start()
    {
        if (TutorialManager.Instance.CurrentState != TutorialManager.TutorialState.Defence)
        {
            gameObject.SetActive(false);
            return;
        }

        StartTutorial();
    }

    private void Update()
    {
        if (step != 1 || unitSelected)
            return;

        if (UnitSpawner.Instance.currentUnit != null)
        {
            unitSelected = true;
            UnitClicked();
        }
    }

    private void StartTutorial()
    {
        step = 0;

        shopHighlight.SetActive(false);
        unitHighlight.SetActive(false);
        inventoryHighlight.SetActive(false);

        StartCoroutine(StartTutorialRoutine());
    }

    private IEnumerator StartTutorialRoutine()
    {
        tutorialText.text = "몰려오는 몬스터를 막아보겠습니다.";

        yield return new WaitForSeconds(3f);

        shopHighlight.SetActive(true);
        tutorialText.text = "하단의 상점 탭에서 유닛을 소환하세요";
    }

    public void ShopClicked()
    {
        if (step != 0)
            return;

        step = 1;

        shopHighlight.SetActive(false);
        unitHighlight.SetActive(true);

        tutorialText.text = "소환된 유닛을 선택해보세요";
    }

    public void UnitClicked()
    {
        if (step != 1)
            return;

        step = 2;

        unitHighlight.SetActive(false);

        StartCoroutine(UnitGuideRoutine());
    }

    private IEnumerator UnitGuideRoutine()
    {
        tutorialText.text = "선택된 유닛은 우클릭을 통해 위치를 변경할 수 있습니다.";

        yield return new WaitForSeconds(3f);

        inventoryHighlight.SetActive(true);

        tutorialText.text = "또한 우측 아이템 창에 있는 아이템을 통해 아이템을 장착하거나, 제거할 수 있습니다.";

        yield return new WaitForSeconds(3f);

        inventoryHighlight.SetActive(false);

        tutorialText.text = "이제 아이템을 활용해서 몬스터를 막아보세요!";

        TutorialManager.Instance.CurrentState = TutorialManager.TutorialState.GameMenu_Forge;

        GameFlowManager.Instance.StartGame();

        yield return new WaitForSeconds(3f);

        tutorialText.gameObject.SetActive(false);
    }
}