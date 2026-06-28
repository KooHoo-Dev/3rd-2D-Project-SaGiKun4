using UnityEngine;

public class MenuBackgroundController : MonoBehaviour
{
    [SerializeField] private GameObject nightAsset;
    [SerializeField] private GameObject afternoonAsset;

    private void Start()
    {
        bool clear = GameManager.Instance.IsAdventureClear;

        nightAsset.SetActive(clear);
        afternoonAsset.SetActive(!clear);
    }
}