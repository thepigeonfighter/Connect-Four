using System;
using System.Collections.Generic;
using System.Linq;
using ConnectFour.AI;
using ConnectFour.AI.AI_Friar;
using UnityEngine;

namespace ConnectFour.AI.AI_Friar
{
    public class CheckMovesToWin
    {
        private GameState _gameState;
        private BoardPosition[,] currentBoardState;
        private TeamName _myTeam;

        public int[,] GetMoves(GameState gameState, TeamName myTeam)
        {
            //Init here since can't use constructors
            currentBoardState = gameState.CurrentBoardState;
            _myTeam = myTeam;
            _gameState = gameState;

            var movesToWin = new int[7, 6];

            for (var v = 0; v < 6; v++)
            {
                for (var h = 0; h < 7; h++)
                {
                    //If unoccupied, get sequences to check
                    if (!gameState.CurrentBoardState[h, v].IsOccupied)
                    {
                        var minMoves = new List<int>();

                        List<BoardPosition> hSequence = GetHorizontalSequence(gameState.CurrentBoardState[h, v]);
                        if (hSequence.Count > 0)
                            minMoves.Add(GetMinMovesInSequence(hSequence));

                        List<BoardPosition> vSequence = GetVerticalSequence(gameState.CurrentBoardState[h, v]);
                        if (vSequence.Count > 0)
                            minMoves.Add(GetMinMovesInSequence(vSequence));

                        List<BoardPosition> diagSequenceA = GetDiagonalSequenceA(gameState.CurrentBoardState[h, v]);
                        if (diagSequenceA.Count > 0)
                            minMoves.Add(GetMinMovesInSequence(diagSequenceA));

                        List<BoardPosition> diagSequenceB = GetDiagonalSequenceB(gameState.CurrentBoardState[h, v]);
                        if (diagSequenceB.Count > 0)
                            minMoves.Add(GetMinMovesInSequence(diagSequenceB));

                        if (minMoves.Count > 0)
                            movesToWin[h, v] = minMoves.Min();
                    }
                }
            }
            return movesToWin;
        }

        #region Horizontal sequence finder
        private List<BoardPosition> GetHorizontalSequence(BoardPosition tile)
        {
            var result = new List<BoardPosition>();
            var row = tile.YIndex;
            var col = tile.XIndex;

            for (var i = 0; i < 7; i++)
            {
                if (!_gameState.CurrentBoardState[i, row].IsOccupied)
                {
                    if (Math.Abs(i - col) < 4)
                        result.Add(currentBoardState[i, row]);
                }
                else if (currentBoardState[i, row].Owner == _myTeam)
                {
                    if (Math.Abs(i - col) < 4)
                        result.Add(currentBoardState[i, row]);
                }
                else if (result.Count < 4)
                {
                    result.Clear();
                }
            }

            //If sequence isn't long enough to form a win or isn't connected to target tile, it is invalid
            if (result.Count < 4 || !result.Contains(tile))
            {
                result.Clear();
            }

            return result;
        }
        #endregion
        #region Vertical sequence finder
        private List<BoardPosition> GetVerticalSequence(BoardPosition tile)
        {
            var result = new List<BoardPosition>();
            var row = tile.YIndex;
            var col = tile.XIndex;

            for (var i = 0; i < 6; i++)
            {
                if (!_gameState.CurrentBoardState[col, i].IsOccupied)
                {
                    if (Math.Abs(i - row) < 4)
                        result.Add(currentBoardState[col, i]);
                }
                else if (currentBoardState[col, i].Owner == _myTeam)
                {
                    if (Math.Abs(i - row) < 4)
                        result.Add(currentBoardState[col, i]);
                }
                else if (result.Count < 4)
                {
                    result.Clear();
                }
            }

            //If sequence isn't long enough to form a win or isn't connected to target tile, it is invalid
            if (result.Count < 4 || !result.Contains(tile))
            {
                result.Clear();
            }

            return result;
        }
        #endregion
        #region Diagonal sequence A finder
        private List<BoardPosition> GetDiagonalSequenceA(BoardPosition tile)
        {
            //This diagonal line is /
            var result = new List<BoardPosition>();
            var row = tile.YIndex;
            var col = tile.XIndex;

            //Adjust row to bottom left-most position
            while (row - 1 >= 0 && col - 1 >= 0)
            {
                row--;
                col--;
            }

            //Adjust up if enemy tile in this position
            while (true)
            {
                if (_gameState.CurrentBoardState[col, row].IsOccupied && _gameState.CurrentBoardState[col, row].Owner != _myTeam)
                {
                    col++;
                    row++;
                }
                else
                    break;
            }

            //To figure out max length of diagonal line, we will use smaller of maxC-C & maxR-R
            var maxLength = 7 - col < 6 - row ? 7 - col : 6 - row;

            for (var i = 0; i < maxLength; i++)
            {
                if (!_gameState.CurrentBoardState[col, row].IsOccupied)
                {
                    if (Math.Abs(col - tile.XIndex) < 4)
                        result.Add(currentBoardState[col, row]);
                }
                else if (currentBoardState[col, row].Owner == _myTeam)
                {
                    if (Math.Abs(col - tile.XIndex) < 4)
                        result.Add(currentBoardState[col, row]);
                }
                else if (result.Count < 4)
                {
                    result.Clear();
                }
                col++;
                row++;
            }

            //If sequence isn't long enough to form a win or isn't connected to target tile, it is invalid
            if (result.Count < 4 || !result.Contains(tile))
            {
                result.Clear();
            }

            return result;
        }
        #endregion
        #region Diagonal sequence B finder
        private List<BoardPosition> GetDiagonalSequenceB(BoardPosition tile)
        {
            //This diagonal line is \
            var result = new List<BoardPosition>();
            var row = tile.YIndex;
            var col = tile.XIndex;

            //Adjust row to bottom right-most position
            while (row - 1 >= 0 && col + 1 <= 6)
            {
                row--;
                col++;
            }

            //Adjust up if enemy tile in this position
            while (true)
            {
                if (_gameState.CurrentBoardState[col, row].IsOccupied && _gameState.CurrentBoardState[col, row].Owner != _myTeam)
                {
                    col--;
                    row++;
                }
                else
                    break;
            }

            //To figure out max length of diagonal line, we will use smaller of maxC-C & maxR-R (mirrored)
            var revCol = Math.Abs(6 - col);
            var maxLength = 7 - revCol < 6 - row ? 7 - revCol : 6 - row;

            for (var i = 0; i < maxLength; i++)
            {
                if (!_gameState.CurrentBoardState[col, row].IsOccupied)
                {
                    if (Math.Abs(col - tile.XIndex) < 4)
                        result.Add(currentBoardState[col, row]);
                }
                else if (currentBoardState[col, row].Owner == _myTeam)
                {
                    if (Math.Abs(col - tile.XIndex) < 4)
                        result.Add(currentBoardState[col, row]);
                }
                else if (result.Count < 4)
                {
                    result.Clear();
                }
                col--;
                row++;
            }

            //If sequence isn't long enough to form a win or isn't connected to target tile, it is invalid
            if (result.Count < 4 || !result.Contains(tile))
            {
                result.Clear();
            }

            return result;
        }
        #endregion

        private int GetMinMovesInSequence(List<BoardPosition> sequence)
        {
            //Return the lowest number of moves that can be made in any series of 4 reachable tiles in the provided sequence
            if (sequence.Count < 4)
                throw new InvalidOperationException("Sequence must be at least 4 items in length");

            int operations = 1 + (sequence.Count - 4);
            var moveCounts = new List<int>();

            int numMoves = 0;

            for (var i = 0; i < operations; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    if (!sequence[i + j].IsOccupied)
                        numMoves++;
                }
                moveCounts.Add(numMoves);
                numMoves = 0;
            }

            return moveCounts.Min();
        }
    }
}
