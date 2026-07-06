
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController Instance;
    [SerializeField] private Image HPBar;
    [SerializeField] private TMP_Text HPText;

    [SerializeField] private TMP_Text heightText;
    [SerializeField] private TMP_Text maxHeightText;
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

    private void Start()
    {
        maxHeightText.text = $"{PlayerBehaviour.Instance.maxHeight}m";
    }

    private void Update()
    {
        UpdateHeightUI();
    }

    public void UpdateHealthUI()
    {
        HPBar.fillAmount = (float)PlayerBehaviour.Instance.currentHp / (float)PlayerBehaviour.Instance.maxHp;
        HPText.SetText($"{PlayerBehaviour.Instance.currentHp}");
    }

    private void UpdateHeightUI()
    {
        float height = PlayerBehaviour.Instance.transform.position.y - heightStandard.position.y;

        float warningHeight = PlayerBehaviour.Instance.maxHeight - 30f;

        heightText.SetText($"{height:F1} m");

        float ratio = Mathf.InverseLerp(warningHeight, PlayerBehaviour.Instance.maxHeight, height);

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