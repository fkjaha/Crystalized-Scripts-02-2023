using System.Collections.Generic;
using UnityEngine;

public class UpgradesUI : MonoBehaviour
{
    [SerializeField] private UpgradableSlotUI slotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private UpgradesManager upgradesManager;

    private List<UpgradableSlotUI> _slots;
    private List<AUpgradable> _relatedUpgradables;

    public void Initialize(List<AUpgradable> upgradables)
    {
        _relatedUpgradables = upgradables;
        SpawnSlots();
        UpdateAllSlotsVisuals();
        CurrencyHolder.Instance.OnCurrencyChanged += UpdateAllSlotsVisuals;
    }

    private void SpawnSlots()
    {
        _slots = new();
        for (int i = 0; i < _relatedUpgradables.Count; i++)
        {
            UpgradableSlotUI slot = Instantiate(slotPrefab, slotsParent);
            _slots.Add(slot);
            InitializeSlot(i);
            UpdateSlotVisuals(i);
        }
    }

    private void InitializeSlot(int slotIndex)
    {
        _slots[slotIndex].Initialize(() =>
        {
            CallForUpgrade(slotIndex);
            UpdateSlotVisuals(slotIndex);
        }, _relatedUpgradables[slotIndex].GetInfo);
    }

    private void CallForUpgrade(int upgradableIndex)
    {
        upgradesManager.RequestForUpgrade(_relatedUpgradables[upgradableIndex]);
    }

    private void UpdateSlotVisuals(int slotIndex)
    {
        _slots[slotIndex].UpdateCostText("" + _relatedUpgradables[slotIndex].GetCost);
        _slots[slotIndex].SetButtonInteractability(CurrencyHolder.Instance.GetCurrency >= _relatedUpgradables[slotIndex].GetCost);
    }

    private void UpdateAllSlotsVisuals()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            UpdateSlotVisuals(i);
        }
    }
}
