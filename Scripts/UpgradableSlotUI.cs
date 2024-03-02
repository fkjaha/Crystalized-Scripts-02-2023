using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradableSlotUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private TextMeshProUGUI costText;

    public void Initialize(UnityAction action, UpgradableInfo info)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            action?.Invoke();
        });
        UpdateSlotView(info);
    }
    
    private void UpdateSlotView(UpgradableInfo info)
    {
        label.text = info.GetLabel;
        button.interactable = true;
    }

    public void UpdateCostText(string newText)
    {
        costText.text = newText;
    }

    public void SetButtonInteractability(bool newState)
    {
        button.interactable = newState;
    }
}
