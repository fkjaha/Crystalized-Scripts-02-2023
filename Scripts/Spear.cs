using System.Collections;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private SpearAnimator spearAnimator;

    public void HitCrystal(SpearsDamageDealer spearsDamageDealer)
    {
        spearAnimator.PlayAnimation();
        StartCoroutine(DamageWithAnimation(spearsDamageDealer));
    }

    private IEnumerator DamageWithAnimation(SpearsDamageDealer spearsDamageDealer)
    {
        yield return new WaitForSeconds(spearAnimator.GetFirstHalfAnimationTime);
        spearsDamageDealer.DamageCrystal();
    }
}
