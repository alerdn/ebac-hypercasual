using Ebac.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Transform container;
    // public List<GameObject> levels;
    public List<LevelPieceBaseSetup> levelPieceBaseSetups;

    private int _index;
    private GameObject _currentLevel;
    public List<LevelPieceBase> _spawnedlevelPieces = new List<LevelPieceBase>();
    public LevelPieceBaseSetup _currSetup;

    private void Start()
    {
        CreateLevelPieces();
        _index = 0;
    }

    public void NextLevel()
    {
        CreateLevelPieces();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) CreateLevelPieces();
    }

    //private void SpawnNexLevel()
    //{
    //    if (_currentLevel != null)
    //    {
    //        Destroy(_currentLevel);
    //        _index++;

    //        if (_index >= levels.Count)
    //        {
    //            ResetLevelIndex();
    //        }
    //    }

    //    _currentLevel = Instantiate(levels[_index], container);
    //    _currentLevel.transform.localPosition = Vector3.zero;
    //}

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    #region PIECES

    private void CreateLevelPieces()
    {
        CleanSpawnedPieces();

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

        ColorManager.Instance.ChangeColorByType(_currSetup.artType);
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

        foreach (var p in spawnedPiece.GetComponentsInChildren<ArtPiece>())
        {
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_currSetup.artType).gameObject);
        }

        _spawnedlevelPieces.Add(spawnedPiece);
    }

    private void CleanSpawnedPieces()
    {
        for (int i = _spawnedlevelPieces.Count - 1; i >= 0; i--)
        {
            Destroy(_spawnedlevelPieces[i].gameObject);
        }

        _spawnedlevelPieces.Clear();
    }

    #endregion
}
