using UnityEngine;

public class SpearsDamageDealer : AUpgradable
{
    [SerializeField] private int damageIncreasePerLevel;
    [SerializeField] private int startDamage;

    private int _damage;

    public void DamageCrystal()
    {
        Level.Instance.GetCrystal.GetDamage(_damage);
        // Debug.Log(_damage, this);
    }

    private protected override void ApplyLevel()
    {
        _damage = startDamage + damageIncreasePerLevel * level;
    }
}
