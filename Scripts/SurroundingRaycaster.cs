using UnityEngine;

public class SurroundingRaycaster : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Transform origin;
    [SerializeField] private LayerMask raycastLayerMask;
    [Space(20f)]
    [SerializeField] private Vector2 xRotationBounds;
    [SerializeField] private Vector2 yRotationBounds;
    [SerializeField] private Vector2 zRotationBounds;
    
    public GameObject GetRandomSurroundingRaycastResult()
    {
        return GetRaycastHitResult(GetRandomRay());
    }
    
    public GameObject GetTargetedSurroundingRaycastResult(Vector3 target)
    {
        return GetRaycastHitResult(GetTargetedRay(ref target));
    }

    private GameObject GetRaycastHitResult(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, radius, raycastLayerMask))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private Ray GetRandomRay()
    {
        return GetRay(GetRandomSurroundingPosition());
    }

    private Ray GetTargetedRay(ref Vector3 target)
    {
        return GetRay(GetTargetedSurroundingPosition(ref target));
    }

    private Ray GetRay(Vector3 surroundingPosition)
    {
        return new Ray(surroundingPosition, origin.position - surroundingPosition);
    }
    
    private Vector3 GetRandomSurroundingPosition()
    {
        return origin.position + new Vector3(
                   GetRandomRotationInBounds(xRotationBounds), 
                   GetRandomRotationInBounds(yRotationBounds),
                   GetRandomRotationInBounds(zRotationBounds)
                   ).normalized
               * radius;
    }
    
    private Vector3 GetTargetedSurroundingPosition(ref Vector3 target)
    {
        return origin.position + (target - origin.position).normalized * radius;
    }

    private float GetRandomRotationInBounds(Vector2 bounds)
    {
        return Random.Range(bounds.x, bounds.y);
    }
}
