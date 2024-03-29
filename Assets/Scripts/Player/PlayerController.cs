using Ebac.Core;
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
    [SerializeField] private Transform target;
    [SerializeField] private float lerpSpeed = 1f;
    [SerializeField] private Vector2 _limits = new(-4, 4);

    [Header("Tags")]
    [SerializeField] private string tagToCheckEnemy = "Enemy";
    [SerializeField] private string tagToCheckEndLine = "EndLine";

    [Header("UI")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text uiTextPowerUp;

    [Header("Animation")]
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private float _scaleDuration = .5f;
    [SerializeField] private Ease _ease = Ease.OutBack;

    [Header("VFX")]
    [SerializeField] private ParticleSystem _vfxDeath;


    private Vector3 _pos;
    private bool _canRun;
    private float _currentSpeed;
    private bool _invencible;
    private Vector3 _startPosition;
    private BounceHelper _bounceHelper;

    private void Start()
    {
        _startPosition = transform.position;
        _bounceHelper = GetComponent<BounceHelper>();
        transform.DOScale(0, _scaleDuration).From().SetEase(_ease);
        ResetSpeed();
    }

    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        if (_pos.x < _limits.x) _pos.x = _limits.x;
        else if (_pos.x > _limits.y) _pos.x = _limits.y;

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
            NextLevel();
        }
    }

    public void Bounce()
    {
        _bounceHelper.Bounce();
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
        _vfxDeath.Play();
    }

    public void NextLevel()
    {
        transform.position = _startPosition;
        ResetSpeed();
        LevelManager.Instance.NextLevel();
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
