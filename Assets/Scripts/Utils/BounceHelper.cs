using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHelper : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float _scaleDuration = .2f;
    [SerializeField] private float _scaleBounce = 1.2f;
    [SerializeField] private Ease _ease = Ease.OutBack;

    public void Bounce()
    {
        transform.DOScale(_scaleBounce, _scaleDuration).SetEase(_ease).SetLoops(2, LoopType.Yoyo);
    }
}
