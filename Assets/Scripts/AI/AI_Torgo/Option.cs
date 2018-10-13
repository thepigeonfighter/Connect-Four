using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ConnectFour.AI.AI_Torgo
{
    public class Option
    {
        public int TotalScore { get { return GetTotalScore(); } private set { TotalScore = value; } }
        public BoardPosition MyPosition { get; set; }
        public bool IsTarget { get; set; }
		public List<Target> Targets { get; set; }
        public int AvailableTargets { get; set; }
        public int FourCost { get; set; }
        public int EFourCost { get; set; }
        public int ConsequenceCost { get; set; }
        private int GetTotalScore()
        {
            return AvailableTargets - FourCost - EFourCost - ConsequenceCost;
        }
    }
}