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
            target.Source = sourcePosition;
            _sourcePosition = sourcePosition;
            int x = sourcePosition.XIndex;
            int y = sourcePosition.YIndex;
            switch (targetName)
            {
                case TargetNames.Target_East:
                    target.Path = GetPath(3, 0);
                    target.TargetPosition = _currentBoard[x + 3, y];
                    break;
                case TargetNames.Target_SouthEast:
                    target.Path = GetPath(3, -3);
                    target.TargetPosition = _currentBoard[x + 3, y - 3];
                    break;
                case TargetNames.Target_South:
                    target.Path = GetPath(0, -3);
                    target.TargetPosition = _currentBoard[x, y - 3];
                    break;
                case TargetNames.Target_SouthWest:
                    target.Path = GetPath(-3, -3);
                    target.TargetPosition = _currentBoard[x - 3, y - 3];
                    break;
                case TargetNames.Target_West:
                    target.Path = GetPath(-3, 0);
                    target.TargetPosition = _currentBoard[x - 3, y];
                    break;
                case TargetNames.Target_NorthWest:
                    target.Path = GetPath(-3, 3);
                    target.TargetPosition = _currentBoard[x - 3, y + 3];
                    break;
                case TargetNames.Target_North:
                    target.Path = GetPath(0, 3);
                    target.TargetPosition = _currentBoard[x, y + 3];
                    break;
                case TargetNames.Target_NorthEast:
                    target.Path = GetPath(3, 3);
                    target.TargetPosition = _currentBoard[x + 3, y + 3];
                    break;


            }
            return target;
        }
        private List<BoardPosition> GetPath(int xDiff, int yDiff)
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
                    newX += _sourcePosition.XIndex;
                }
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
                    newY += _sourcePosition.YIndex;
                }
                path.Add(_currentBoard[newX, newY]);

            }

            return path;
        }
    }
}
