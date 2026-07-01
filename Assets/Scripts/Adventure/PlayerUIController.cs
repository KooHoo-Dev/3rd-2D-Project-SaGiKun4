
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController Instance;
    [SerializeField] private Image HPBar;
    [SerializeField] private TMP_Text HPText;
    [SerializeField] private PlayerBehaviour player;

    [SerializeField] private TMP_Text heightText;
    [SerializeField] private Transform heightStandard;

    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color warningColor = Color.red;

    [SerializeField] private float flashSpeed = 6f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        UpdateHeightUI();
    }

    public void UpdateHealthUI()
    {
        HPBar.fillAmount = (float)player.currentHp / (float)player.maxHp;
        HPText.SetText($"{player.currentHp}");
    }

    private void UpdateHeightUI()
    {
        float height = player.transform.position.y - heightStandard.position.y;

        float warningHeight = player.maxHeight - 30f;

        heightText.SetText($"{height:F1} m");

        float ratio = Mathf.InverseLerp(warningHeight, player.maxHeight, height);

        if (ratio < 1f)
        {
            heightText.color = Color.Lerp(normalColor, warningColor, ratio);
        }
        else
        {
            float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
            heightText.color = Color.Lerp(normalColor, warningColor, t);
        }
    }
}