using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CurrencyRenderer : MonoBehaviour
{
    [SerializeField] private CurrencyHolder currencyHolder;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private float counterTime;

    private void Awake()
    {
        currencyHolder.OnCurrencyChangedWithChangeData += UpdateCurrencyText;
    }

    private void UpdateCurrencyText(int oldValue, int newValue)
    {
        currencyText.DOComplete();
        currencyText.DOCounter(oldValue, newValue, counterTime);
    }
}
