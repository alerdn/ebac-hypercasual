using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererHelper : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private List<Transform> _positions;

    private void Start()
    {
        _lineRenderer.positionCount = _positions.Count;
    }

    private void Update()
    {
        for (int i = 0; i < _positions.Count; i++)
        {
            _lineRenderer.SetPosition(i, _positions[i].position);
        }
    }
}
