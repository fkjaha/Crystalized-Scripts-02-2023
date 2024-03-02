using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SpearsStorage : AUpgradable
{
    public event UnityAction OnSpearsUpdated;
    public event UnityAction<Transform> OnSpearAdded;

    public List<Spear> GetSpears => _spears;

    [SerializeField] private Spear spearPrefab;
    [SerializeField] private Transform spearsParent;
    [SerializeField] private int spearsPerLevel;
    [SerializeField] private SpearsPositionController spearsPositionController;

    private List<Spear> _spears = new();
    
    private protected override void ApplyLevel()
    {
        CalibrateSpearsCount();
        spearsPositionController.PlaceSpears(_spears.Select(spear => spear.transform).ToList());
        OnSpearsUpdated?.Invoke();
    }

    private void CalibrateSpearsCount()
    {
        if(_spears.Count == spearsPerLevel * level) return;

        int delta = spearsPerLevel * level - _spears.Count;
        
        for (int i = 0; i < Mathf.Abs(delta); i++)
        {
            if(delta > 0)
                AddOneSpear();
            else 
                RemoveOneSpear();
        }
    }

    private void AddOneSpear()
    {
        Spear spear = Instantiate(spearPrefab, spearsParent);
        _spears.Add(spear);
        OnSpearAdded?.Invoke(spear.transform);
    }
    
    private void RemoveOneSpear()
    {
        Spear spear = _spears[0];
        _spears.RemoveAt(0);
        Destroy(spear.gameObject);
    }
}
