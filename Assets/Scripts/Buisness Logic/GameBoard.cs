using System;
using System.Collections;
using System.Collections.Generic;
using ConnectFour.BuisnessLogic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    #region  Public Vars
    public GameObject BottomCollider;
    public EventHandler OnBoardFullEvent;
    #endregion

    #region  Private Vars
    private BoardPosition[,] _currentBoardState;
    private IBoardBuilder _builder;
    private List<ColumnIndex> _allColumns = new List<ColumnIndex>();
    private bool[] _isColumnFull = new bool[7];
    private bool _boardFull;
    #endregion

    #region Public Utility Methods
    public bool IsValidMove(Move move)
    {

        if (_isColumnFull[(int)move.Column])
        {
            return false;
        }
        if (_boardFull)
        {
            return false;
        }
        return true;
    }

    public BoardPosition[,] GetCurrentBoard()
    {
        return _currentBoardState;
    }
    public void SetMovement(Move move)
    {
        int columnIndex = (int)move.Column;
        for (int i = 0; i < 6; i++)
        {
            if (!_currentBoardState[columnIndex, i].IsOccupied)
            {
                Vector2 position = _currentBoardState[columnIndex, i].Position;
                BoardPosition bp = new BoardPosition()
                {
                    YIndex = i,
                    XIndex = columnIndex,
                    Owner = move.MyTeam,
                    IsOccupied = true,
                    Position = position,
                    TimeSet = DateTime.Now,
                    Player = move.Player

                };
                _currentBoardState[columnIndex, i] = bp;
                break;
            }
        }
        CheckColumn((int)move.Column);
    }

    public List<float> GetColumnPositions()
    {
        List<float> output = new List<float>();
        for (int i = 0; i < 7; i++)
        {
            output.Add(_currentBoardState[i, 0].Position.x);
        }
        return output;
    }
    public void ClearBoard()
    {
        BottomCollider.SetActive(false);
        _isColumnFull = new bool[7];
        _currentBoardState = _builder.BuildBoard();
        Invoke("EnableBottomCollider", 3f);
    }
    public List<ColumnIndex> GetAvailableColumns()
    {
        List<ColumnIndex> output = new List<ColumnIndex>();
        foreach (ColumnIndex index in _allColumns)
        {
            if (!_isColumnFull[(int)index])
            {
                output.Add(index);
            }
        }
        return output;
    }
    public List<Vector2> GetListofBoardPositionsInGameSpace(List<BoardPosition> boardPositions)
    {
        List<Vector2> vectors = new List<Vector2>();
        foreach (BoardPosition bp in boardPositions)
        {
            BoardPosition actualBoardPosition = _currentBoardState[bp.XIndex, bp.YIndex];
            vectors.Add(actualBoardPosition.Position);
        }
        return vectors;
    }
    #endregion
    #region  Private Internal State Managment
    private void Start()
    {
        _builder = new BoardBuilder();
        _currentBoardState = _builder.BuildBoard();
        BuildColumnList();
    }
    private void CheckColumn(int columnIndex)
    {

        int piecesInColumn = 0;
        for (int i = 0; i < 6; i++)
        {
            if (_currentBoardState[columnIndex, i].IsOccupied)
            {
                piecesInColumn++;
            }
        }
        if (piecesInColumn == 6)
        {
            _isColumnFull[columnIndex] = true;
        }
        CheckForFullBoard();
    }
    private void CheckForFullBoard()
    {
        foreach (bool b in _isColumnFull)
        {
            if (!b)
            {
                return;
            }
        }
        _boardFull = true;
        OnBoardFullEvent?.Invoke(this, EventArgs.Empty);

    }
    private void EnableBottomCollider()
    {
        _boardFull = false;
        BottomCollider.SetActive(true);
    }
    //Terrible code think of a way to fix be my guest
    private void BuildColumnList()
    {
        _allColumns = new List<ColumnIndex>()
        {
            ColumnIndex.Zero,
            ColumnIndex.One,
            ColumnIndex.Two,
            ColumnIndex.Three,
            ColumnIndex.Four,
            ColumnIndex.Five,
            ColumnIndex.Six
        };
    }
    #endregion
}
