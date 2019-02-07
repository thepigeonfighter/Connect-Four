using System.Collections.Generic;
using UnityEngine;

namespace ConnectFour.AI.AI_FoulWind
{
    public class TreeBuilder
    {
        private int childCounter = 0;
        private BoardScorer boardScorer = new BoardScorer();
        private Position[] nodes;
        private TeamName _myTeam;
        public TreeBuilder(TeamName myTeam, int depth)
        {
            int nodeCount = Mathf.RoundToInt(Mathf.Pow(8, depth ));
            nodes = new Position[nodeCount];
            BoardPosition[,] emptyBoard = new BoardPosition[7, 6];
            for (int i = 0; i < nodeCount; i++)
            {
                nodes[i] = new Position(emptyBoard);
            }
            _myTeam = myTeam;
        }
        public void BuildChildren(Position parent, BoardPosition[,] currentBoardState, TeamName teamName, int depth)
        {
            List<Position> children = new List<Position>();
            if (depth <= 0 || Mathf.Abs(parent.Score) == 100)
            {
                return;
            }
            List<BoardPosition> availableMoves = parent.BoardAtThisState.GetAvailableMoves();
            foreach (var move in availableMoves)
            {
                var tempBoard = parent.BoardAtThisState.CloneBoard();
                UpdateBoard(tempBoard, move, teamName);
                Position pos = nodes[childCounter];
                pos.Parent = parent;
                pos.StaticPosition = move;
                pos.BoardAtThisState = tempBoard;
               // pos.Score = boardScorer.ScoreBoard(tempBoard, _myTeam);
                childCounter++;
                pos.ChildNumber = childCounter;
                children.Add(pos);

            }
            foreach (var child in children)
            {
                BuildChildren(child, child.BoardAtThisState, teamName.GetOppositeTeam(), depth - 1);
            }
            parent.Children = children;

        }
        private int GetParentCount(Position position)
        {
            int count = 0;
            var tempPos = position;
            while(tempPos.Parent != null)
            {
                tempPos = tempPos.Parent;
                count++;
            }
            return count;
        }
        private void UpdateBoard(BoardPosition[,] boardState, BoardPosition move, TeamName teamName)
        {
            boardState[move.XIndex, move.YIndex].IsOccupied = true;
            boardState[move.XIndex, move.YIndex].Owner = teamName;
        }

    }
}
