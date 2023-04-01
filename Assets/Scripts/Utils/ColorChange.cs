using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChange : MonoBehaviour
{
    private Color _startColor = Color.white;
    private float _duration = 2f;
    private Color _originalColor;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _originalColor = _meshRenderer.materials[0].GetColor("_Color");
        LerpColor();
    }

    private void LerpColor()
    {
        _meshRenderer.materials[0].SetColor("_Color", _startColor);
        _meshRenderer.materials[0].DOColor(_originalColor, _duration).SetDelay(.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            LerpColor();
        }
    }
}
