using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelper : MonoBehaviour
{
    [SerializeField] private List<Transform> _positions;
    [SerializeField] private float _duration = 1f;

    private int _index = 1;

    private void Start()
    {
        SetRandomStartPosition();
        StartCoroutine(StartMovement());
    }

    private void SetRandomStartPosition()
    {
        int startIndex = Random.Range(0, _positions.Count);

        _index = startIndex;
        transform.position = _positions[_index].position;
    }

    private IEnumerator StartMovement()
    {
        float time = 0f;

        while (true)
        {
            Vector3 currentPosition = transform.position;

            while (time < _duration)
            {
                transform.position = Vector3.Lerp(currentPosition, _positions[_index].transform.position, (time / _duration));

                time += Time.deltaTime;
                yield return null;
            }

            _index++;

            if (_index >= _positions.Count) _index = 0;
            time = 0f;

            yield return null;
        }
    }
}
