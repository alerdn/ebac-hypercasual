using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;
    public List<LevelPieceBaseSetup> levelPieceBaseSetups;

    private int _index;
    private GameObject _currentLevel;
    private List<LevelPieceBase> _spawnedlevelPieces;
    private LevelPieceBaseSetup _currSetup;

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

        if (_currSetup != null)
        {
            _index++;

            if (_index >= levelPieceBaseSetups.Count)
            {
                ResetLevelIndex();
            }
        }

        _currSetup = levelPieceBaseSetups[_index];

        for (int i = 0; i < _currSetup.piecesStartNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelStartPieces);
        }

        for (int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces);
        }

        for (int i = 0; i < _currSetup.piecesEndNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelEndPieces);
        }
    }

    private void CreateLevelPiece(List<LevelPieceBase> levelPieces)
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

    private void CleanSpawnedPieces()
    {
        int count = _spawnedlevelPieces.Count;
        
        for (int i = 0; i < count; i++)
        {
            Destroy(_spawnedlevelPieces[i]);
        }
    }

    #endregion
}
