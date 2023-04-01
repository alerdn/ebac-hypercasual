using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    public bool collected = false;
    public float lerp = 5f;
    public float minDistance = 1f;

    private void Start()
    {
        CoinAnimatorManager.Instance.RegisterCoin(this);
    }

    private void Update()
    {
        if (collected)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position, lerp * Time.deltaTime);
            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < minDistance)
            {
                HideItens();
                Destroy(gameObject);
            }
        }
    }

    protected override void OnCollect()
    {
        base.OnCollect();
        collected = true;
        PlayerController.Instance.Bounce();
    }

    protected override void Collect()
    {
        OnCollect();
    }
}
