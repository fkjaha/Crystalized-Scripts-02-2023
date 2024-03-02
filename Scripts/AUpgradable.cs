using UnityEngine;
using UnityEngine.Events;

public abstract class AUpgradable: MonoBehaviour
{
    public int GetLevel => level;
    public UpgradableInfo GetInfo => info;
    public int GetCost => _cost;
    
    [SerializeField] private protected int level;
    [SerializeField] private UpgradableInfo info;
    [SerializeField] private UnityEvent onLevelUpgraded;
    [SerializeField] private int startCost;
    [SerializeField] private int costIncreasePerLevel;
    [SerializeField] private int startLevel;
    [SerializeField] private string levelSaveKey;

    private int _cost;

    private protected virtual void Start()
    {
        ApplyLevel();
        UpdateCost();
    }

    public void UpgradeLevelByOne()
    {
        level++;
        onLevelUpgraded.Invoke();
        UpdateCost();
        ApplyLevel();
    }

    private void UpdateCost()
    {
        _cost = startCost + level * costIncreasePerLevel;
    }
    
    private protected abstract void ApplyLevel();

    public void SaveLevel()
    {
        PlayerPrefs.SetInt(levelSaveKey, level);
    }

    public void LoadLevel()
    {
        level = PlayerPrefs.GetInt(levelSaveKey, startLevel);
        ApplyLevel();
    }

    public void ResetLevel()
    {
        PlayerPrefs.DeleteKey(levelSaveKey);
        LoadLevel();
    }
}
