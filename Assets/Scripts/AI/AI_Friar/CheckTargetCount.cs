using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectFour.AI.AI_Friar
{
    public class CheckTargetCount
    {
        private GameState _gameState;
        private BoardPosition[,] currentBoardState;
        private TeamName _myTeam;

        public int[,] GetTargets(GameState gameState, TeamName myTeam)
        {
            //Init here since can't use constructors
            currentBoardState = gameState.CurrentBoardState;
            _myTeam = myTeam;
            _gameState = gameState;

            var targetCount = new int[7, 6];

            for (var v = 0; v < 6; v++)
            {
                for (var h = 0; h < 7; h++)
                {
                    //If unoccupied, get sequences to check
                    if (!gameState.CurrentBoardState[h, v].IsOccupied)
                    {
                        var targets = 0;

                        targets += GetHorizontalTargets(gameState.CurrentBoardState[h, v]);
                        targets += GetVerticalTargets(gameState.CurrentBoardState[h, v]);
                        targets += GetDiagonalTargetsA(gameState.CurrentBoardState[h, v]);
                        targets += GetDiagonalTargetsB(gameState.CurrentBoardState[h, v]);

                        targetCount[h, v] = targets;
                    }
                }
            }
            return targetCount;
        }

        #region Horizontal target finder
        private int GetHorizontalTargets(BoardPosition tile)
        {
            var result = new List<BoardPosition>();
            var targets = 0;
            var row = tile.YIndex;
            var col = tile.XIndex;

            //Check right
            for (var i = 0; i < 4; i++)
            {
                if (col + i > 6)
                    break;

                if (!_gameState.CurrentBoardState[col + i, row].IsOccupied)
                {
                    result.Add(currentBoardState[col + i, row]);
                }
                else if (currentBoardState[col + i, row].Owner == _myTeam)
                {
                    result.Add(currentBoardState[col + i, row]);
                }
            }

            if (result.Count == 4)
                targets++;

            result.Clear();

            //Check left
            for (var i = 0; i < 4; i++)
            {
                if (col - i < 0)
                    break;

                if (!_gameState.CurrentBoardState[col - i, row].IsOccupied)
                {
                    result.Add(currentBoardState[col - i, row]);
                }
                else if (currentBoardState[col - i, row].Owner == _myTeam)
                {
                    result.Add(currentBoardState[col - i, row]);
                }
            }

            if (result.Count == 4)
            {
                targets++;
            }
            return targets;
        }
        #endregion
        #region Vertical target finder
        private int GetVerticalTargets(BoardPosition tile)
        {
            var result = new List<BoardPosition>();
            var targets = 0;
            var row = tile.YIndex;
            var col = tile.XIndex;

            //Check up
            for (var i = 0; i < 4; i++)
            {
                if (row + i > 5)
                    break;

                if (!_gameState.CurrentBoardState[col, row + i].IsOccupied)
                {
                    result.Add(currentBoardState[col, row + i]);
                }
                else if (currentBoardState[col, row + i].Owner == _myTeam)
                {
                    result.Add(currentBoardState[col, row + i]);
                }
            }

            if (result.Count == 4)
                targets++;

            result.Clear();

            //Check down
            for (var i = 0; i < 4; i++)
            {
                if (row - i < 0)
                    break;

                if (!_gameState.CurrentBoardState[col, row - i].IsOccupied)
                {
                    result.Add(currentBoardState[col, row - i]);
                }
                else if (currentBoardState[col, row - i].Owner == _myTeam)
                {
                    result.Add(currentBoardState[col, row - i]);
                }
            }

            if (result.Count == 4)
            {
                targets++;
            }
            return targets;
        }
        #endregion
        #region Diagonal target A finder
        private int GetDiagonalTargetsA(BoardPosition tile)
        {
            //This diagonal line is /
            var result = new List<BoardPosition>();
            var targets = 0;
            var row = tile.YIndex;
            var col = tile.XIndex;

            for (var i = 0; i < 4; i++)
            {
                if (col + 1 > 7 || row + 1 > 6)
                {
                    break;
                }

                if (!_gameState.CurrentBoardState[col, row].IsOccupied)
                {
                    result.Add(currentBoardState[col, row]);
                }
                else if (currentBoardState[col, row].Owner == _myTeam)
                {
                    result.Add(currentBoardState[col, row]);
                }

                col++;
                row++;
            }

            if (result.Count == 4)
                targets++;

            result.Clear();
            col = tile.XIndex;
            row = tile.YIndex;

            for (var i = 0; i < 4; i++)
            {
                if (col - 1 < -1 || row - 1 < -1)
                {
                    break;
                }

                if (!_gameState.CurrentBoardState[col, row].IsOccupied)
                {
                    result.Add(currentBoardState[col, row]);
                }
                else if (currentBoardState[col, row].Owner == _myTeam)
                {
                    result.Add(currentBoardState[col, row]);
                }

                col--;
                row--;
            }

            if (result.Count == 4)
            {
                targets++;
            }
            return targets;
        }
        #endregion
        #region Diagonal target B finder
        private int GetDiagonalTargetsB(BoardPosition tile)
        {
            //This diagonal line is \
            var result = new List<BoardPosition>();
            var targets = 0;
            var row = tile.YIndex;
            var col = tile.XIndex;

            for (var i = 0; i < 4; i++)
            {
                if (col - 1 < -1 || row + 1 > 6)
                {
                    break;
                }

                if (!_gameState.CurrentBoardState[col, row].IsOccupied)
                {
                    result.Add(currentBoardState[col, row]);
                }
                else if (currentBoardState[col, row].Owner == _myTeam)
                {
                    result.Add(currentBoardState[col, row]);
                }

                col--;
                row++;
            }

            if (result.Count == 4)
                targets++;

            result.Clear();
            col = tile.XIndex;
            row = tile.YIndex;

            for (var i = 0; i < 4; i++)
            {
                if (col + 1 > 7 || row - 1 < -1)
                {
                    break;
                }

                if (!_gameState.CurrentBoardState[col, row].IsOccupied)
                {
                    result.Add(currentBoardState[col, row]);
                }
                else if (currentBoardState[col, row].Owner == _myTeam)
                {
                    result.Add(currentBoardState[col, row]);
                }

                col++;
                row--;
            }

            if (result.Count == 4)
            {
                targets++;
            }
            return targets;
        }
        #endregion
    }
}
