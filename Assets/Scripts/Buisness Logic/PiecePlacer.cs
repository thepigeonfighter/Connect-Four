using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PiecePlacer:MonoBehaviour {
    public GameObject BlackPiece, RedPiece;
    private readonly float height = 8f;
    private Dictionary<ColumnIndex, float> _columnIndexPlacementDictionary = new Dictionary<ColumnIndex, float>();
    private GameBoard _gameBoard;
    private GameObject blackPiecesParent;
    private GameObject redPiecesParent;

    private void Start()
	{
        _gameBoard = GetComponent<GameBoard>();
        List<float> indexValues = _gameBoard.GetColumnPositions();
        for (int i = 0; i < indexValues.Count;i++)
		{

            _columnIndexPlacementDictionary.Add((ColumnIndex)i, indexValues[i]);
        }
    }
    public void SetPiece(Move move)
	{
        bool exists =_columnIndexPlacementDictionary.TryGetValue(move.Column, out float xPos);
		if(exists)
		{
  
            GameObject parent = BuildTeamPieceContainer(move.MyTeam);
            if(move.MyTeam == TeamName.RedTeam && !redPiecesParent)
            {
                redPiecesParent = parent;
            }
            else if (move.MyTeam == TeamName.BlackTeam && !blackPiecesParent)
            {
                blackPiecesParent = parent;
            }
            GameObject pieceToPlace = GetPiece(move.MyTeam);
            Instantiate(pieceToPlace, new Vector2(xPos, height), Quaternion.identity, parent.transform);
            
        }

    }
	private GameObject GetPiece(TeamName teamName)
	{
        switch(teamName)
		{
			case TeamName.BlackTeam:
                return BlackPiece;
			case TeamName.RedTeam :
                return RedPiece;
			default:
                return null;

        }

    }
	private GameObject BuildTeamPieceContainer(TeamName teamName)
	{
        GameObject teamPieceContainer = GameObject.Find(teamName.ToString());
		if(teamPieceContainer == null)
		{
            teamPieceContainer = new GameObject(teamName.ToString());
        }
        return teamPieceContainer;
    }
    private void OnDisable()
    {
        if (redPiecesParent)
        {
            Destroy(redPiecesParent);

        }
        if (blackPiecesParent)
        {
            Destroy(blackPiecesParent);
        }
    }
}
