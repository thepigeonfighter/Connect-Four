using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ConnectFour.AI.AI_Torgo
{
    public class TargetBuilder
    {
        private BoardPosition _sourcePosition;
        private BoardPosition[,] _currentBoard;
        public TargetBuilder(BoardPosition[,] currentBoard)
        {
            _currentBoard = currentBoard;
        }
        public Target BuildTarget(BoardPosition sourcePosition, TargetNames targetName)
        {
            Target target = new Target();
            List<BoardPosition> localPath = new List<BoardPosition>();
            target.Source = sourcePosition;
            _sourcePosition = sourcePosition;
            int x = sourcePosition.XIndex;
            int y = sourcePosition.YIndex;
            switch (targetName)
            {
                case TargetNames.Target_East:
                    localPath = GetLocalPath(3, 0);
                    target.TargetPosition = _currentBoard[x + 3, y];
                    break;
                case TargetNames.Target_SouthEast:
                    localPath = GetLocalPath(3, -3);
                    target.TargetPosition = _currentBoard[x + 3, y - 3];
                    break;
                case TargetNames.Target_South:
                    localPath = GetLocalPath(0, -3);
                    target.TargetPosition = _currentBoard[x, y - 3];
                    break;
                case TargetNames.Target_SouthWest:
                    localPath = GetLocalPath(-3, -3);
                    target.TargetPosition = _currentBoard[x - 3, y - 3];
                    break;
                case TargetNames.Target_West:
                    localPath = GetLocalPath(-3, 0);
                    target.TargetPosition = _currentBoard[x - 3, y];
                    break;
                case TargetNames.Target_NorthWest:
                    localPath = GetLocalPath(-3, 3);
                    target.TargetPosition = _currentBoard[x - 3, y + 3];
                    break;
                case TargetNames.Target_North:
                    localPath = GetLocalPath(0, 3);
                    target.TargetPosition = _currentBoard[x, y + 3];
                    break;
                case TargetNames.Target_NorthEast:
                    localPath = GetLocalPath(3, 3);
                    target.TargetPosition = _currentBoard[x + 3, y + 3];
                    break;


            }
            target.Path = localPath;
            target.MovesRequiredToFillPath = GetRequiredMovesList(target.Path);
            return target;
        }
        private List<BoardPosition> GetLocalPath(int xDiff, int yDiff)
        {
            List<BoardPosition> path = new List<BoardPosition>();
            for (int i = 0; i < 3; i++)
            {
                int newX = 0;
                int newY = 0;
                if (xDiff != 0)
                {
                    if (xDiff > 0)
                    {
                        newX = xDiff - i;
                    }
                    else
                    {
                        newX = xDiff + i;
                    }
                    
                }
                newX += _sourcePosition.XIndex;
                if (yDiff != 0)
                {
                    if (yDiff > 0)
                    {
                        newY = yDiff - i;
                    }
                    else
                    {
                        newY = yDiff + i;
                    }
                    
                }
                newY += _sourcePosition.YIndex;
                path.Add(_currentBoard[newX, newY]);

            }

            return path;
        }
        //This will add all the positions that are under the path positions and return a list
        //of moves that will be necessary in order to fill path 
        private List<BoardPosition> GetRequiredMovesList(List<BoardPosition> path)
        {
            List<BoardPosition> allMovesBelowPath = new List<BoardPosition>();
            List<BoardPosition> requiredMoves = new List<BoardPosition>();
            foreach (BoardPosition bp in path)
            {
                for (int i = 1; i < bp.YIndex + 1; i++)
                {
                    BoardPosition pos = _currentBoard[bp.XIndex, bp.YIndex - i];
                    allMovesBelowPath.Add(pos);
                }
            }
            foreach (BoardPosition bp in allMovesBelowPath)
            {
                
                if (!bp.IsOccupied )
                {
                    requiredMoves.Add(bp);
                }
            }


            return requiredMoves;
        }
    }
}
