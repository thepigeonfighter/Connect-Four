using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
namespace ConnectFour
{
    public static class ScoreKeeper
    {

        private static TeamStats _redTeamStats = new TeamStats();
        private static TeamStats _blackTeamStats = new TeamStats();


        public static void UpdateStats(GameResult result)
        {
            switch (result.Winner)
            {
                case TeamName.BlackTeam:
                    UpdateBlackTeamStats(result);
                    break;
                case TeamName.RedTeam:
                    UpdateRedTeamStats(result);
                    break;
            }


        }
        private static void UpdateRedTeamStats(GameResult result)
        {
            if (result.WinType != WinType.Forfeit)
            {
                _redTeamStats.WinCount++;
                _redTeamStats.Player = result.Player;
            }
            
            _redTeamStats = _redTeamStats.AddCorrectWinType(result.WinType);

        }
        private static TeamStats AddCorrectWinType(this TeamStats stats, WinType type)
        {
            switch (type)
            {
                case WinType.Diagonal:
                    stats.DiagonalWins++;
                    break;
                case WinType.Horizontal:
                    stats.HorizontalWins++;
                    break;
                case WinType.Vertical:
                    stats.VerticalWins++;
                    break;
                case WinType.Forfeit:
                    stats.Forfeits++;
                    break;
            }
            return stats;
        }
        private static void UpdateBlackTeamStats(GameResult result)
        {
            if (result.WinType != WinType.Forfeit)
            {
                _blackTeamStats.WinCount++;
                _blackTeamStats.Player = result.Player;
            }
            
            _blackTeamStats = _blackTeamStats.AddCorrectWinType(result.WinType);
        }
        public static TeamStats GetRedTeamStats()
        {
            return _redTeamStats;
        }
        public static TeamStats GetBlackTeamStats()
        {
            return _blackTeamStats;
        }
    }
}
