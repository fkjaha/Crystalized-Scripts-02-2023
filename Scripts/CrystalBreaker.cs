using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CrystalBreaker : MonoBehaviour
{
    public event UnityAction<Transform> OnPartBreakenOff; 

    // [SerializeField] private Crystal crystal;
    [SerializeField] private SurroundingRaycaster crystalRaycaster;
    [SerializeField] private Transform center;
    [SerializeField] private float partFlyForce;

    public void BreakCrystalPart()
    {
        GameObject hitResult;
        Rigidbody hitBody = null;

        hitResult = crystalRaycaster.GetTargetedSurroundingRaycastResult(Level.Instance.GetCrystal.GetTopPart().transform.position);
        if (hitResult != null)
        {
            hitResult.TryGetComponent(out hitBody);
        }
        else Debug.LogWarning("1");

        if (hitBody == null)
        {
            Debug.LogWarning("No crystal part was hit!");
            return;
        }

        BreakCrystalPart(hitBody);
    }

    private void BreakCrystalPart(Rigidbody part)
    {
        Level.Instance.GetCrystal.RemoveCrystalPart(part);
        part.isKinematic = false;
        part.gameObject.layer = 0;
        part.AddForce(
            (part.transform.position - center.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f))).normalized
                      * partFlyForce, ForceMode.Impulse);
        StartCoroutine(DelayedDestroy(part.transform));
        
        OnPartBreakenOff?.Invoke(part.transform);
    }

    private IEnumerator DelayedDestroy(Transform partTransform)
    {
        yield return new WaitForSeconds(2);
        partTransform.DOScale(Vector3.one * .0001f, 2).SetEase(Ease.InOutCirc).onComplete += () =>
        {
            partTransform.DOKill();
            Destroy(partTransform.gameObject);
        };
    }
}
