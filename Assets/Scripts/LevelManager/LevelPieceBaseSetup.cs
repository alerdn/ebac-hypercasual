using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPieceBaseSetup : MonoBehaviour
{
    [Header("Pieces")]
    public List<LevelPieceBase> levelStartPieces;
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelEndPieces;
    
    public int piecesStartNumber = 5;
    public int piecesNumber = 5;
    public int piecesEndNumber = 5;
}
