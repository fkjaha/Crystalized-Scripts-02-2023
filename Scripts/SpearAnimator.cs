using System;
using DG.Tweening;
using UnityEngine;

public class SpearAnimator : MonoBehaviour
{
    public float GetFirstHalfAnimationTime => firstHalfAnimationTime;
    
    [SerializeField] private Transform animationTransform;
    [SerializeField] private Vector3 animationMoveDirection;
    [SerializeField] private float firstHalfAnimationTime;
    [SerializeField] private float secondHalfAnimationTime;
    [SerializeField] private Ease firstHalfEase;
    [SerializeField] private Ease secondHalfEase;
    
    public void PlayAnimation()
    {
        ResetActiveAnimations();

        animationTransform.DOLocalMove(animationMoveDirection, firstHalfAnimationTime).SetEase(firstHalfEase)
                .onComplete +=
            () =>
            {
                animationTransform.DOLocalMove(Vector3.zero, secondHalfAnimationTime).SetEase(secondHalfEase);
            };
    }

    private void ResetActiveAnimations()
    {
        animationTransform.DOComplete();
        animationTransform.DOKill();

        animationTransform.localPosition = Vector3.zero;
    }

    private void OnDestroy()
    {
        animationTransform.DOKill();
    }
}
