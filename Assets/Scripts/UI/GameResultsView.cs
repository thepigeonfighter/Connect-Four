using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ConnectFour
{
    public class GameResultsView : MonoBehaviour
    {
        public Text RedTeamWinCount, BlackTeamWinCount, RedTeamWintypeDisplay, BlackTeamWinTypeDisplay;
        public Text RedTeamPlayedByText, BlackTeamPlayedByText;

        void Update()
        {
            BuildView();
        }
        void BuildView()
        {
            TeamStats redTeamStats = ScoreKeeper.GetRedTeamStats();
            TeamStats blackTeamStats = ScoreKeeper.GetBlackTeamStats();
            RedTeamWinCount.text = BuildWinCountDisplay(redTeamStats);
            BlackTeamWinCount.text = BuildWinCountDisplay(blackTeamStats);
            RedTeamWintypeDisplay.text = BuildWinTypeDisplay(redTeamStats);
            BlackTeamWinTypeDisplay.text = BuildWinTypeDisplay(blackTeamStats);
            RedTeamPlayedByText.text = BuildPlayedByText(redTeamStats);
            BlackTeamPlayedByText.text = BuildPlayedByText(blackTeamStats);
        }
        string BuildWinCountDisplay(TeamStats stats)
        {
            string winCountMessage = $"Wins -- {stats.WinCount}";
            return winCountMessage;
        }
        string BuildWinTypeDisplay(TeamStats stats)
        {
            string wintypeDisplay = $" Horizontal Wins -- {stats.HorizontalWins} \n Vertical Wins -- {stats.VerticalWins} \n Diagonal Wins -- {stats.DiagonalWins} \n Forfeitures {stats.Forfeits}";
            return wintypeDisplay;
        }
        string BuildPlayedByText(TeamStats stats)
        {
            string playedByText = "";
            if (stats.Player != null)
            {
                playedByText = $"Played by:   {stats.Player.GetName()}";
            }
            else
            {
                playedByText =  "Played by:";
            }
            return playedByText;
        }
    }
}