using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpearsPositionController : MonoBehaviour
{
    public enum PlacementMode
    {
        EqualDistance,
        Queued
    }
    
    [SerializeField] private Transform origin;
    [SerializeField] private float placingRadius;
    [SerializeField] private SpearsStorage spearsStorage;
    [SerializeField] private Vector3 startPlacementVectorPosition;
    [SerializeField] private PlacementMode placementMode;
    [SerializeField] private float transformChangingTime;
    [SerializeField] private Ease transformChangeEase;

    [Header("Queued Mode")] 
    [SerializeField] private float defaultAngleSpacing;

    public void PlaceSpears(List<Transform> spears)
    {
        for (int i = 0; i < spears.Count; i++)
        {
            PlaceSpearByIndex(spears[i], i, spears.Count);
        }
    }

    private void PlaceSpearByIndex(Transform spear, int index, int spearsCount)
    {
        Vector3 targetPosition = GetPositionByIndex(index, spearsCount);
        Quaternion targetRotation = Quaternion.LookRotation(origin.position - targetPosition, Vector3.up);
        
        if (IsObjectLastInList(index, spearsCount))
        {
            Vector3 initialPosition = GetPositionByIndex(index - 1, spearsCount);
            spear.position = initialPosition;
        }
        ChangePositionWithLerp(spear, targetPosition, targetRotation);
    }

    private bool IsObjectLastInList(int index, int listCount)
    {
        return index == listCount - 1;
    }

    private void ChangePositionWithLerp(Transform target, Vector3 targetPosition, Quaternion targetRotation)
    {
        target.DOKill();
        target.DOMove(targetPosition, transformChangingTime).SetEase(transformChangeEase);
        target.DORotateQuaternion(targetRotation, transformChangingTime).SetEase(transformChangeEase);
    }

    private Vector3 GetPositionByIndex(int index, int spearsCount)
    {
        Vector3 position = Vector3.zero;

        if (placementMode == PlacementMode.EqualDistance || spearsCount * defaultAngleSpacing >= 360)
        {
            position = origin.position + Quaternion.AngleAxis(360f / spearsCount * index, Vector3.up) *
                startPlacementVectorPosition * placingRadius;
        }
        else if (placementMode == PlacementMode.Queued)
        {
            position = origin.position + Quaternion.AngleAxis(index * defaultAngleSpacing, Vector3.up) *
                startPlacementVectorPosition * placingRadius;
        }

        return position;
    }

    private void OnDrawGizmosSelected()
    {
        if(origin == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin.position, placingRadius);
    }
}
