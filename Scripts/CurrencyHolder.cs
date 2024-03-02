using System;
using UnityEngine;
using UnityEngine.Events;

public class CurrencyHolder : MonoBehaviour
{
    public static CurrencyHolder Instance;

    public event UnityAction<int, int> OnCurrencyChangedWithChangeData; 
    public event UnityAction OnCurrencyChanged; 

    public int GetCurrency => currency;
    
    [SerializeField] private int currency;
    [SerializeField] private int startCurrencyAmount;
    [SerializeField] private string saveKey;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Level.Instance.OnLevelCompleted += Reset;
        if (AppEvents.Instance != null)
        {
            AppEvents.Instance.OnGameClosed += Save;
            AppEvents.Instance.OnGamePaused += Save;   
        }
        Load();
        OnCurrencyChangedWithChangeData?.Invoke(0, currency);
    }

    public void AddCurrency(int amount)
    {
        ChangeCurrency(amount);
    }

    public bool TrySpendCurrency(int amount)
    {
        if (currency >= amount)
        {
            ChangeCurrency(-amount);
            return true;
        }

        return false;
    }

    private void ChangeCurrency(int delta)
    {
        int currencyOld = currency;
        currency += delta;
        OnCurrencyChangedWithChangeData?.Invoke(currencyOld, currency);
        OnCurrencyChanged?.Invoke();
    }
    
    private void Save()
    {
        PlayerPrefs.SetInt(saveKey, currency);
    }

    private void Load()
    {
        currency = PlayerPrefs.GetInt(saveKey, startCurrencyAmount);
    }

    private void Reset()
    {
        PlayerPrefs.DeleteKey(saveKey);
        Load();
    }
}
