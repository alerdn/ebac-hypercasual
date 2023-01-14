using Ebac.Code;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    public float speed = 1f;
    public GameObject coinCollector;

    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Tags")]
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    [Header("UI")]
    public GameObject endScreen;
    public TMP_Text uiTextPowerUp;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    private Vector3 _pos;
    private bool _canRun;
    private float _currentSpeed;
    private bool _invencible;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
    }

    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(_currentSpeed * Time.deltaTime * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tagToCheckEnemy))
        {
            if (!_invencible)
            {
                MoveBack();
                EndGame(AnimatorManager.AnimationType.DEAD);
            }
        }
        else if (collision.transform.CompareTag(tagToCheckEndLine))
        {
            EndGame();
        }
    }

    private void MoveBack()
    {
        transform.DOMoveZ(-1, .1f).SetRelative();
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / speed);
    }

    public void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    #region POWER UP
    private void SetPowerUpText(string t)
    {
        uiTextPowerUp.text = t;
    }


    public void PowerUpSeedUp(float s)
    {
        _currentSpeed = s;
        SetPowerUpText("Speeding up");
    }

    public void ResetSpeed()
    {
        _currentSpeed = speed;
        SetPowerUpText("");
    }

    public void SetInvencible(bool b = true)
    {
        _invencible = b;
        if (b) SetPowerUpText("Invencible");
        else SetPowerUpText("");
    }

    public void ChangeHeight(float h, float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y + h, animationDuration).SetEase(ease);
        SetPowerUpText("Getting higher");
    }

    public void ResetHeight(float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y, animationDuration).SetEase(ease);
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
        if (amount > 1.5f) SetPowerUpText("Vacuuming coins");
    }

    #endregion
}
