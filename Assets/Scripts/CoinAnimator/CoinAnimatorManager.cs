using DG.Tweening;
using Ebac.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinAnimatorManager : Singleton<CoinAnimatorManager>
{
    [Header("Animation")]
    [SerializeField] private float _scaleDuration = .2f;
    [SerializeField] private float _scaleTimeBetweenPieces = .1f;
    [SerializeField] private Ease _ease = Ease.OutBack;

    public List<ItemCollectableCoin> _coins;

    private void Start()
    {
        _coins = new List<ItemCollectableCoin>();
    }

    public void RegisterCoin(ItemCollectableCoin coin)
    {
        if (!_coins.Contains(coin))
        {
            _coins.Add(coin);
            coin.transform.localScale = Vector3.zero;
        }
    }

    public void ClearCoins()
    {
        _coins.Clear();
    }

    public void StartAnimations()
    {
        StartCoroutine(ScalePiecesByTime());
    }

    private IEnumerator ScalePiecesByTime()
    {
        Sort();
        for (int i = 0; i < _coins.Count; i++)
        {
            var coin = _coins[i];
            if (coin == null) continue;

            coin.transform.DOScale(1, _scaleDuration).SetEase(_ease);
            yield return new WaitForSeconds(_scaleTimeBetweenPieces);
        }
    }

    private void Sort()
    {
        _coins = _coins.OrderBy((coin) => Vector3.Distance(transform.position, coin.transform.position)).ToList();
    }
}
