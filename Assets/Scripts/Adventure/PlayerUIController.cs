
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController Instance;
    [SerializeField] private Image HPBar;
    [SerializeField] private TMP_Text HPText;
    [SerializeField] private PlayerBehaviour player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void UpdateHealthUI()
    {
        HPBar.fillAmount = (float)player.currentHp / (float)player.maxHp;
        HPText.SetText($"{player.currentHp}");
    }
}