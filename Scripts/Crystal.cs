using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Crystal : MonoBehaviour
{
    public event UnityAction OnBroken;
    public event UnityAction OnGetDamage;
    
    [SerializeField] private List<Rigidbody> crystalParts;
    [SerializeField] private int startCrystalCost;
    [SerializeField] private int startHp;

    private List<Rigidbody> _crystalPartsSortedByHeight;
    private int _singlePartCost;
    private int _startNumberOfParts;
    private int _hp;
    private int _cost;
    private bool _isBroken;

    private void Start()
    {
        _hp = startHp;
        _cost = startCrystalCost;
        _startNumberOfParts = crystalParts.Count;
        _singlePartCost = startCrystalCost / _startNumberOfParts;
        FillSortedPartsList();
    }

#if UNITY_EDITOR
    [ContextMenu("Set Up This Object As Crystal")]
    private void SetUpThisObjectAsCrystal()
    {
        List<Rigidbody> parts = transform.GetComponentsInChildren<Rigidbody>().ToList();
        crystalParts = new();
        crystalParts.AddRange(parts);
        foreach (Rigidbody crystalPart in crystalParts)
        {
            crystalPart.isKinematic = true;
            BoxCollider boxCollider = crystalPart.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            crystalPart.gameObject.layer = 3;
        }

        gameObject.layer = 3;
    }
#endif

    public Rigidbody GetRandomCrystalPart()
    {
        return crystalParts[Random.Range(0, crystalParts.Count)];
    }

    public Rigidbody GetTopPart()
    {
        return _crystalPartsSortedByHeight[0];
    }

    public bool IsCrystalPart(Rigidbody part)
    {
        return crystalParts.Contains(part);
    }

    public void RemoveCrystalPart(Rigidbody part)
    {
        if (!IsCrystalPart(part)) return;

        crystalParts.Remove(part);
        _crystalPartsSortedByHeight.Remove(part);
        UpdateCrystalState();
        ReturnSinglePartCostToCurrencyHolder();
    }

    public void GetDamage(int damage)
    {
        _hp -= damage;
        CheckIfHpCanCarryCurrentNumberOfParts();
        OnGetDamage?.Invoke();
    }

    private void CheckIfHpCanCarryCurrentNumberOfParts()
    {
        int numberOfCurrentMaxPartsPossible = (int)(_hp * (float)_startNumberOfParts / startHp);
        if(crystalParts.Count <= numberOfCurrentMaxPartsPossible) return;
        CallCrystalPartsReduction(crystalParts.Count - numberOfCurrentMaxPartsPossible);
    }

    private void CallCrystalPartsReduction(int delta)
    {
        for (int i = 0; i < delta; i++)
        {
            Level.Instance.GetCrystalBreaker.BreakCrystalPart();
        }
    }

    private void FillSortedPartsList()
    {
        _crystalPartsSortedByHeight = crystalParts.OrderByDescending(part => part.transform.position.y).ToList();
    }

    private void ReturnSinglePartCostToCurrencyHolder()
    {
        if (crystalParts.Count == 0)
        {
            CurrencyHolder.Instance.AddCurrency(_cost);
            _cost = 0;
        } 
        else
        {
            _cost -= _singlePartCost;
            CurrencyHolder.Instance.AddCurrency(_singlePartCost);
        }
    }

    private void UpdateCrystalState()
    {
        bool wasBroken = _isBroken;
        _isBroken = crystalParts.Count == 0;
        if (!wasBroken && _isBroken)
        {
            OnBroken?.Invoke();
        }
    }
}
