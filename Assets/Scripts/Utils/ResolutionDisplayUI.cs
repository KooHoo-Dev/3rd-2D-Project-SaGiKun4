using TMPro;
using UnityEngine;

public class ResolutionDropdownUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    private void OnEnable()
    {
        UpdateDropdownLabel();
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    private void OnDisable()
    {
        dropdown.onValueChanged.RemoveListener(OnDropdownChanged);
    }

    public void UpdateDropdownLabel()
    {
        if (dropdown.options.Count > 0)
        {
            dropdown.options[0].text = OptionManager.Instance.GetResolutionText();
            dropdown.RefreshShownValue();
        }
    }

    private void OnDropdownChanged(int index)
    {
        OptionManager.Instance.SelectResolution(index);

        UpdateDropdownLabel();
    }
}