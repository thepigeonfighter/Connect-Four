using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ConnectFour.AI.AI_Torgo
{
	public enum TargetNames { Target_North, Target_NorthEast, Target_East, Target_SouthEast, Target_South, Target_SouthWest, Target_West, Target_NorthWest }
    public class Target
    {
        public List<BoardPosition> Path { get; set; }
		public BoardPosition Source { get; set; }
		public BoardPosition TargetPosition { get; set; }
		public bool IsValidTarget { get; set; }
        public bool CheckIfTargetValid(BoardPosition[,] currentBoard, TeamName myTeam)
		{
			foreach(BoardPosition bp in Path)
			{
                BoardPosition boardPosition = currentBoard[bp.XIndex, bp.YIndex];
                if( boardPosition.IsOccupied && boardPosition.Owner != myTeam)
				{
                    return false;
                }
			}
            return true;
        }
		public int GetFourCost(BoardPosition[,] currentBoard, TeamName myTeam)
		{
            int tempFourCost = 4;
			foreach(BoardPosition bp in Path)
			{
				if(currentBoard[bp.XIndex,bp.YIndex].Owner == myTeam)
				{
                    tempFourCost--;
                }
			}
            return tempFourCost;
        }
		public BoardPosition GetNextPosition(BoardPosition[,] currentBoard)
		{
			if(!currentBoard[Source.XIndex,Source.YIndex].IsOccupied)
			{
                return currentBoard[Source.XIndex, Source.YIndex];
            }
			else{
				foreach(BoardPosition bp in Path)
				{
					if(!bp.IsOccupied)
					{
                        return bp;
                    }
				}
			}
            return null;
        }
		

    }
}
