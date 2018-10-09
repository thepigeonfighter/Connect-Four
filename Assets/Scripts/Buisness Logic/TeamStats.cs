using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ConnectFour
{
    public class TeamStats
    {
        public IPlayer Player { get; set; }
        public int WinCount { get; set; }
        public int VerticalWins { get; set; }
        public int HorizontalWins { get; set; }
        public int DiagonalWins { get; set; }
    }
}
