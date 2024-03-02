using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Instance;

    [SerializeField] private UpgradesUI upgradesUI;
    [SerializeField] private List<AUpgradable> upgradables;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        upgradesUI.Initialize(upgradables);

        Level.Instance.OnLevelCompleted += ResetAllUpgradables;
        if (AppEvents.Instance != null)
        {
            AppEvents.Instance.OnGamePaused += SaveAllUpgradables;
            AppEvents.Instance.OnGameClosed += SaveAllUpgradables;
        }
        LoadAllUpgradables();
    }

    public void RequestForUpgrade(AUpgradable upgradable)
    {
        if(!upgradables.Contains(upgradable)) return;
        
        if (CurrencyHolder.Instance.TrySpendCurrency(upgradable.GetCost))
        {
            UpgradeUpgradable(upgradable);
        }
    }
    
    private void UpgradeUpgradable(AUpgradable upgradable)
    {
        upgradable.UpgradeLevelByOne();
    }

    private void SaveAllUpgradables()
    {
        foreach (AUpgradable upgradable in upgradables)
        {
            upgradable.SaveLevel();
        }
    }

    private void LoadAllUpgradables()
    {
        foreach (AUpgradable upgradable in upgradables)
        {
            upgradable.LoadLevel();
        }
    }

    private void ResetAllUpgradables()
    {
        foreach (AUpgradable upgradable in upgradables)
        {
            upgradable.ResetLevel();
        }
    }
}
