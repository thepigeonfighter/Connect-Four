using System.Collections.Generic;
using UnityEngine;

namespace ConnectFour.AI.AI_FoulWind
{
    public class Position
    {
        public Position Parent { get; set; }
        public List<Position> Children { get; set; }
        public BoardPosition[,] BoardAtThisState { get; set; }
        public BoardPosition StaticPosition { get; set; }
        public int Score { get; set; }
        public int ChildNumber { get; set; }
        public Position(BoardPosition[,] boardState)
        {
            Children = new List<Position>();
            BoardAtThisState = boardState;
        }
           

    }

}