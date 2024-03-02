using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearsHitClient : AUpgradable
{
    [SerializeField] private SpearsStorage spearsStorage;
    [SerializeField] private SpearsDamageDealer spearsDamageDealer;
    [SerializeField] private float frequencyReductionPerLevel;
    [SerializeField] private float startHitFrequency;
    [SerializeField] private float timeBetweenSeparateSpearsActivation;

    private float _frequency;
    private bool _enabled;

    private protected override void Start()
    {
        base.Start();
        _enabled = true;
        Level.Instance.OnLevelCompleted += () => _enabled = false;
        StartCoroutine(HitCaller());
    }
    
    private protected override void ApplyLevel()
    {
        _frequency = startHitFrequency - frequencyReductionPerLevel * level;
        if (_frequency < 0) _frequency = 0;
    }

    private IEnumerator HitCaller()
    {
        while (_enabled)
        {
            yield return new WaitForSeconds(_frequency);
            StartCoroutine(CallSeparateSpearsOneByOne());
        }
    }
    
    private IEnumerator CallSeparateSpearsOneByOne()
    {
        List<Spear> spears = spearsStorage.GetSpears;
        for (int i = 0; i < spears.Count; i++)
        {
            spears[i].HitCrystal(spearsDamageDealer);
            yield return new WaitForSeconds(timeBetweenSeparateSpearsActivation);
        }
    }
}
