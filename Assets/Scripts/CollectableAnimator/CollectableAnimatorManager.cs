using DG.Tweening;
using Ebac.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectableAnimatorManager : Singleton<CollectableAnimatorManager>
{
    [Header("Animation")]
    [SerializeField] private float _scaleDuration = .2f;
    [SerializeField] private float _scaleTimeBetweenPieces = .1f;
    [SerializeField] private Ease _ease = Ease.OutBack;

    public List<ItemCollectableBase> _collectables;

    private void Start()
    {
        _collectables = new List<ItemCollectableBase>();
    }

    public void RegisterCollectable(ItemCollectableBase collectable)
    {
        if (!_collectables.Contains(collectable))
        {
            _collectables.Add(collectable);
            collectable.transform.localScale = Vector3.zero;
        }
    }

    public void ClearCoins()
    {
        _collectables.Clear();
    }

    public void StartAnimations()
    {
        StartCoroutine(ScalePiecesByTime());
    }

    private IEnumerator ScalePiecesByTime()
    {
        Sort();
        for (int i = 0; i < _collectables.Count; i++)
        {
            var collectable = _collectables[i];
            if (collectable == null) continue;

            collectable.transform.DOScale(1, _scaleDuration).SetEase(_ease);
            yield return new WaitForSeconds(_scaleTimeBetweenPieces);
        }
    }

    private void Sort()
    {
        _collectables = _collectables
            .FindAll((collectable) => collectable != null)
            .OrderBy((collectable) => Vector3.Distance(transform.position, collectable.transform.position)).ToList();
    }
}
