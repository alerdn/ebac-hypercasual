using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPieces;
    public int piecesNumber = 5;

    private int _index;
    private GameObject _currentLevel;
    private List<LevelPieceBase> _spawnedlevelPieces;

    private void Awake()
    {
        //SpawnNexLevel();
        CreateLevelPieces();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) SpawnNexLevel();
    }

    private void SpawnNexLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if (_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region PIECES

    private void CreateLevelPieces()
    {
        _spawnedlevelPieces = new List<LevelPieceBase>();

        for (int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece();
        }
    }

    private void CreateLevelPiece()
    {
        var piece = levelPieces[Random.Range(0, levelPieces.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if (_spawnedlevelPieces.Count > 0)
        {
            var lastPiece = _spawnedlevelPieces[^1];
            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }

        _spawnedlevelPieces.Add(spawnedPiece);
    }

    #endregion
}
