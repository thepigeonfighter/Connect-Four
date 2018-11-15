using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace ConnectFour
{
    public class CheckForWin
    {
        #region Private Vars
        private enum DiagonalDirection { LeftToRight, RightToLeft };
        private List<BoardPosition> _blackWinningPositions = new List<BoardPosition>();
        private List<BoardPosition> _redWinningPositions = new List<BoardPosition>();
        #endregion
        public GameResult CheckWin(BoardPosition[,] boardState)
        {
            GameResult result = GetDefaultGameResult();
            List<BoardPosition>[] pieces = GetPiecesInPlayByTeam(boardState);
            if (AreThereEnoughPiecesInPlayForAWin(pieces) && IsWinPossible(boardState))
            {
                foreach (List<BoardPosition> bps in pieces)
                {
                    if (CheckForHorizontalWin(bps))
                    {
                        result = GetHorizontalWin(bps);
                        break;
                    }
                    else if (CheckForVerticalWin(bps))
                    {
                        result = GetVerticalWin(bps);
                        break;
                    }
                    else if (CheckForDiagonalWin(bps))
                    {
                        result = GetDiagonalWin(bps);
                        break;
                    }
                }
            }
            return result;

        }
        #region Build Game Results
        private GameResult GetDefaultGameResult()
        {
            GameResult gameResult = new GameResult()
            {
                GameStatus = GameStatus.InProgress,

            };
            return gameResult;
        }
        private GameResult GetHorizontalWin(List<BoardPosition> boardPositions)
        {
            GameResult result = new GameResult()
            {
                GameStatus = GameStatus.Completed,
                Winner = boardPositions[0].Owner,
                WinType = WinType.Horizontal,
                Player = boardPositions[0].Player,
                WinningPositions = GetWinningPieces(boardPositions[0].Owner)
            };
            return result;
        }
        private GameResult GetVerticalWin(List<BoardPosition> boardPositions)
        {
            GameResult result = new GameResult()
            {
                GameStatus = GameStatus.Completed,
                Winner = boardPositions[0].Owner,
                WinType = WinType.Vertical,
                Player = boardPositions[0].Player,
                WinningPositions = GetWinningPieces(boardPositions[0].Owner)
            };
            return result;
        }
        private GameResult GetDiagonalWin(List<BoardPosition> boardPositions)
        {
            GameResult result = new GameResult()
            {
                GameStatus = GameStatus.Completed,
                Winner = boardPositions[0].Owner,
                WinType = WinType.Diagonal,
                Player = boardPositions[0].Player,
                WinningPositions = GetWinningPieces(boardPositions[0].Owner)
            };
            return result;
        }
        #endregion
        #region PreChecks 
        //This method checks to make sure that there is a chance for a win 
        //Before doing a complex check
        private bool IsWinPossible(BoardPosition[,] boardState)
        {
            if (boardState[3, 0].IsOccupied)
            {
                return true;
            }
            if (boardState[0, 3].IsOccupied)
            {
                return true;
            }
            if (boardState[1, 3].IsOccupied)
            {
                return true;
            }
            if (boardState[2, 3].IsOccupied)
            {
                return true;
            }
            if (boardState[4, 3].IsOccupied)
            {
                return true;
            }
            if (boardState[5, 3].IsOccupied)
            {
                return true;
            }
            if (boardState[6, 3].IsOccupied)
            {
                return true;
            }
            return false;
        }
        private bool AreThereEnoughPiecesInPlayForAWin(List<BoardPosition>[] pieces)
        {
            if (pieces[0].Count > 3 || pieces[1].Count > 3)
            {
                return true;
            }
            return false;
        }
        #endregion
        #region Piece Filter
        private List<BoardPosition>[] GetPiecesInPlayByTeam(BoardPosition[,] boardState)
        {
            List<BoardPosition> teamRedSpots = new List<BoardPosition>();
            List<BoardPosition> teamBlackSpots = new List<BoardPosition>();
            foreach (BoardPosition bp in boardState)
            {
                if (bp.IsOccupied)
                {
                    if (bp.Owner == TeamName.BlackTeam)
                    {
                        teamBlackSpots.Add(bp);
                    }
                    else
                    {
                        teamRedSpots.Add(bp);
                    }
                }
            }
            List<BoardPosition>[] output = new List<BoardPosition>[] { teamBlackSpots, teamRedSpots };
            return output;
        }
        #endregion
        #region  Horizontal Win Checks
        public bool CheckForHorizontalWin(List<BoardPosition> boardPositions)
        {
            //Check row
            List<List<BoardPosition>> positionsOrderedByRow = new List<List<BoardPosition>>();
            int height = boardPositions.Max(x => x.YIndex);
            for (int i = 0; i < height + 1; i++)
            {
                List<BoardPosition> row = boardPositions.Where(x => x.YIndex == i).ToList();
                positionsOrderedByRow.Add(row);
            }
            foreach (List<BoardPosition> bps in positionsOrderedByRow)
            {
                if (CheckRows(bps))
                {
                    return true;
                }
            }

            return false;

        }
        private bool CheckRows(List<BoardPosition> row)
        {
            if (row.Count > 3)
            {
                //Player needs to have middle of the row in order for a chance at a horizontal win
                BoardPosition centerOfRow = row.FirstOrDefault(x => x.XIndex == 3);
                // centerOfRow.AddPossibleWinningPiece();
                if (centerOfRow != null)
                {

                    int minX = row.Min(x => x.XIndex);
                    int maxX = row.Max(x => x.XIndex);
                    //This fifthindex crap is to catch a silly edge case 
                    BoardPosition fifthIndex = row.FirstOrDefault(x => x.XIndex == 5);
                    if (minX + 3 < maxX && fifthIndex != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            BoardPosition piece = row.FirstOrDefault(x => x.XIndex == maxX - i);
                            if (piece == null)
                            {
                                ResetWinningPositions(centerOfRow);
                                return false;
                            }
                            else
                            {
                                AddPossibleWinningPiece(piece);
                            }
                        }
                    }
                    //Check to make sure the player has the next three pieces
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            BoardPosition piece = row.FirstOrDefault(x => x.XIndex == minX + i);
                            if (piece == null)
                            {
                                ResetWinningPositions(centerOfRow);
                                return false;
                            }
                            else
                            {
                                AddPossibleWinningPiece(piece);
                            }
                        }
                    }
                    return true;

                }
            }
            return false;
        }
        #endregion
        #region  Vertical Win Checks
        public bool CheckForVerticalWin(List<BoardPosition> boardPositions)
        {
            List<List<BoardPosition>> positionsOrderedByColumn = new List<List<BoardPosition>>();
            int height = boardPositions.Max(x => x.XIndex);
            for (int i = 0; i < height + 1; i++)
            {
                List<BoardPosition> row = boardPositions.Where(x => x.XIndex == i).ToList();
                positionsOrderedByColumn.Add(row);
            }
            foreach (List<BoardPosition> bps in positionsOrderedByColumn)
            {
                if (CheckColumns(bps))
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckColumns(List<BoardPosition> columns)
        {
            if (columns.Count > 3)
            {
                BoardPosition centerOfColumn = columns.FirstOrDefault(x => x.YIndex == 3);
                //centerOfColumn.AddPossibleWinningPiece();
                if (centerOfColumn != null)
                {
                    int minX = columns.Min(x => x.YIndex);
                    int maxX = columns.Max(x => x.YIndex);
                    if (minX + 3 < maxX)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            BoardPosition piece = columns.FirstOrDefault(x => x.YIndex == maxX - i);
                            if (piece == null)
                            {
                                return false;
                            }
                            else
                            {
                                ResetWinningPositions(centerOfColumn);
                                AddPossibleWinningPiece(piece);
                            }
                        }
                    }
                    //Check to make sure the player has the next three pieces
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            BoardPosition piece = columns.FirstOrDefault(x => x.YIndex == minX + i);
                            if (piece == null)
                            {
                                ResetWinningPositions(centerOfColumn);
                                return false;
                            }
                            else
                            {
                                AddPossibleWinningPiece(piece);
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        #endregion
        #region  Diagonal Win Checks
        public bool CheckForDiagonalWin(List<BoardPosition> boardPositions)
        {
            if (LeftToRightDiagonalWin(boardPositions))
            {
                return true;
            }
            else
            {
                return RightToLeftDiagonalWin(boardPositions);
            }

        }
        private bool LeftToRightDiagonalWin(List<BoardPosition> boardPositions)
        {
            List<BoardPosition> originPieces = new List<BoardPosition>();
            foreach (BoardPosition bp in boardPositions)
            {
                if (bp.XIndex < 4 && bp.YIndex < 3)
                {
                    originPieces.Add(bp);
                }
            }

            return CheckOriginPieces(originPieces, boardPositions, DiagonalDirection.LeftToRight);
        }
        private bool RightToLeftDiagonalWin(List<BoardPosition> boardPositions)
        {
            List<BoardPosition> originPieces = new List<BoardPosition>();
            foreach (BoardPosition bp in boardPositions)
            {
                if (bp.XIndex < 4 && bp.YIndex > 2)
                {
                    originPieces.Add(bp);
                }
            }

            return CheckOriginPieces(originPieces, boardPositions, DiagonalDirection.RightToLeft);
        }
        private bool CheckOriginPieces(List<BoardPosition> originPieces, List<BoardPosition> boardPositions, DiagonalDirection direction)
        {

            for (int i = 0; i < originPieces.Count; i++)
            {
                AddPossibleWinningPiece(originPieces[i]);
                bool winFound = true;
                for (int j = 1; j < 4; j++)
                {

                    int newX = originPieces[i].XIndex + j;
                    int newY = originPieces[i].YIndex;
                    if (direction == DiagonalDirection.LeftToRight)
                    {
                        newY += j;
                    }
                    else
                    {
                        newY -= j;
                    }
                    BoardPosition piece = boardPositions.FirstOrDefault(x => x.XIndex == newX && x.YIndex == newY);
                    if (piece == null)
                    {
                        winFound = false;
                    }
                    else
                    {
                        AddPossibleWinningPiece(piece);
                    }
                }
                if (winFound)
                {
                    return true;
                }
                if (originPieces.Count > 0)
                {
                    ResetWinningPositions(originPieces[i]);
                }
            }

            return false;
        }
        #endregion 
        #region  Handle Internal State
        private void AddPossibleWinningPiece( BoardPosition piece)
        {
            switch (piece.Owner)
            {
                case TeamName.BlackTeam:
                    if (_blackWinningPositions.Count <= 3)
                    {
                        _blackWinningPositions.Add(piece);
                    }
                    break;

                case TeamName.RedTeam:
                    if (_redWinningPositions.Count <= 3)
                    {
                        _redWinningPositions.Add(piece);
                    }
                    break;
            }
        }
        private void ResetWinningPositions(BoardPosition piece)
        {
            switch (piece.Owner)
            {
                case TeamName.BlackTeam:
                    _blackWinningPositions.Clear();
                    break;
                case TeamName.RedTeam:
                    _redWinningPositions.Clear();
                    break;
            }
        }
        private List<BoardPosition> GetWinningPieces(TeamName name)
        {
            switch (name)
            {
                case TeamName.BlackTeam:
                    return _blackWinningPositions;
                case TeamName.RedTeam:
                    return _redWinningPositions;
                default:
                    return null;
            }
        }
        #endregion
    }
}