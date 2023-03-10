using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollectableBase
{
    [Header("Power Up")]
    public float duration;

    protected override void OnCollect()
    {
        base.OnCollect();
        StartPowerUp();
    }

    protected virtual void StartPowerUp()
    {
        Invoke(nameof(EndPowerUp), duration);
    }

    protected virtual void EndPowerUp() { }
}
