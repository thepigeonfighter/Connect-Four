using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace ConnectFour.AI.AI_Torgo
{
	public enum TargetNames { Target_North, Target_NorthEast, Target_East, Target_SouthEast, Target_South, Target_SouthWest, Target_West, Target_NorthWest }
    public class Target
    {
        public List<BoardPosition> Path { get; set; }
		public BoardPosition Source { get; set; }
		public BoardPosition TargetPosition { get; set; }
		public List<BoardPosition> MovesRequiredToFillPath{get;set;}
		public bool IsValidTarget { get; set; }
        public bool CheckIfTargetValid(BoardPosition[,] currentBoard, TeamName myTeam)
		{
			foreach(BoardPosition bp in Path)
			{
                BoardPosition boardPosition = currentBoard[bp.XIndex, bp.YIndex];
                if( boardPosition.IsOccupied && boardPosition.Owner != myTeam)
				{
                  //  Debug.Log($"Position owned by {boardPosition.Owner} being looked at by {myTeam}");
                    return false;
                }
			}
            return true;
        }

		public int GetFourCost(BoardPosition[,] currentBoard, TeamName myTeam)
		{
            int fourCost = MovesRequiredToFillPath.Count;
            List<BoardPosition> unFilledPathPositions = Path.Where(x => x.IsOccupied == false ).ToList();
            fourCost += unFilledPathPositions.Count;

            return fourCost;
        }
        public int FourChance(BoardPosition[,] currentBoard, TeamName myTeam)
        {
            int fourCompleted = 0;
            foreach (var bp in Path)
            {
                if(currentBoard[bp.XIndex,bp.YIndex].IsOccupied)
                {
                    fourCompleted++;
                }
            }
            fourCompleted *= 4;
            return fourCompleted;
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
        public override bool Equals(object obj)
        {
            if(obj ==null || this.GetType() != obj.GetType())
            {
                return false;
            }
            Target target = (Target)obj;
            return target.TargetPosition == this.TargetPosition;
        }
        public override int GetHashCode()
        {
            return (TargetPosition.XIndex << 3) ^ TargetPosition.YIndex;
        }

    }
}
