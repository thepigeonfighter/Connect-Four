using System.Collections.Generic;

namespace ConnectFour.AI
{
    public static class AIHelper
    {
        public static int MovesRequiredToReachPosition(this BoardPosition bp, BoardPosition[,] currentBoard)
        {
            int x = bp.XIndex;
            int y = bp.YIndex;
            int movesRequired = 0;
            for (int i = y; i >= 0; i--)
            {
                if (currentBoard[x, i].IsOccupied)
                {
                    break;
                }
                movesRequired++;
            }
            return movesRequired;
        }
        public static TeamName GetOppositeTeam(this TeamName teamName)
        {
            if(teamName == TeamName.RedTeam) { return TeamName.BlackTeam; }
            return TeamName.RedTeam;
        }
        public static List<BoardPosition> GetAvailableMoves(this BoardPosition[,] currentBoard)
        {
            List<BoardPosition> availableMoves = new List<BoardPosition>();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (!currentBoard[i, j].IsOccupied)
                    {
                        availableMoves.Add(currentBoard[i, j]);
                        break;
                    }
                }
            }
            return availableMoves;

        }
        public static BoardPosition[,] CloneBoard(this BoardPosition[,] sourceBoard)
        {
            BoardPosition[,] newBoard = new BoardPosition[7, 6];
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    newBoard[i, j] = new BoardPosition
                    {
                        IsOccupied = sourceBoard[i, j].IsOccupied,
                        Owner = sourceBoard[i, j].Owner,
                        XIndex = sourceBoard[i, j].XIndex,
                        YIndex = sourceBoard[i, j].YIndex
                    };
                }
            }
            return newBoard;
        }
        public static BoardPosition GetBoardPositionByColumn(this ColumnNumber columnNumber, BoardPosition[,] currentBoard)
        {
            for (int i = 0; i < 6; i++)
            {
                if(!currentBoard[columnNumber,i].IsOccupied)
                {
                    return currentBoard[columnNumber, i];
                }
            }
            return null;
        }
    }
}