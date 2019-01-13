using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace ConnectFour
{
    public class StatsView : MonoBehaviour, IStatsView
    {
        public Text WinningMessage;
        public Text DebugBoardState;
        public GameObject MovesLabel;
        public Toggle DebugBoardStateToggle;
        public Toggle MoveListViewToggle;
        public GameObject MovesParent;
        public GameObject MoveTextPrefab;
        private bool DebugBoardStateView;
        private bool MoveListView = true;
        public GameObject scrollBar;



        public void UpdateDebugBoardState(GameState gameState)
        {
            if (DebugBoardStateView)
            {
                List<string> boardPostions = GetUnformattedBoardState(gameState);
                List<string> formattedList = FormatBoardStateToDoubleColumn(boardPostions.ToArray());
                DebugBoardState.text = BuildFormattedList(formattedList);
            }
            if (!DebugBoardStateView)
            {
                DebugBoardState.text = "";
            }
        }

        private GameObject CreateMovePrefab(string message)
        {
            GameObject gb = Instantiate(MoveTextPrefab, Vector2.zero, Quaternion.identity, MovesParent.transform);
            Text gbText = gb.GetComponent<Text>();
            gbText.text = message;
            return gb;
        }
        private void DisplayMoveList(MoveEvent move)
        {

            string message = $"{move.MyMove.MyTeam} chose column number {move.MyMove.Column}";
            CreateMovePrefab(message);

        }

        public void DisplayWinMessage(GameResult result, int turnCount)
        {
            string winType = GetWinTypeWord(result.WinType);
            string winWord = AdjectiveHolder.GetVictoryWord();//
            WinningMessage.text = ($"{GetAdjective(turnCount)} {winType} {winWord} by {result.Player.GetName()} in {turnCount} turns");
        }
        private List<string> GetUnformattedBoardState(GameState gameState)
        {
            List<string> boardPostions = new List<string>();
            foreach (BoardPosition bp in gameState.CurrentBoardState)
            {
                boardPostions.Add($"({bp.XIndex},{bp.YIndex}) {bp.Owner} ");
            }
            return boardPostions;
        }
        private string BuildFormattedList(List<string> formattedList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            formattedList.ForEach(x => stringBuilder.Append(x));
            string boardState = stringBuilder.ToString();
            boardState = boardState.Replace("@", System.Environment.NewLine);
            return boardState;
        }
        private List<string> FormatBoardStateToDoubleColumn(string[] allPositions)
        {
            string[] firstCol = allPositions.Take((allPositions.Length) / 2).ToArray();
            string[] secondCol = allPositions.Skip((allPositions.Length) / 2).ToArray();
            List<string> formattedList = new List<string>();
            for (int i = 0; i < firstCol.Length; i++)
            {
                formattedList.Add($"{firstCol[i]} {secondCol[i]} @");
            }
            return formattedList;
        }
        private string GetWinTypeWord(WinType winType)
        {
            switch (winType)
            {
                case WinType.Diagonal:
                    return AdjectiveHolder.GetDiagonalWord();
                case WinType.Horizontal:
                    return AdjectiveHolder.GetHorizontalWord();
                case WinType.Vertical:
                    return AdjectiveHolder.GetVerticalWord();
                default:
                    return null;
            }
        }
        private string GetAdjective(int turnCount)
        {
            if (turnCount < 15)
            {
                return AdjectiveHolder.GetGoodAdjective();
            }
            else if (turnCount < 30)
            {
                return AdjectiveHolder.GetOkAdjective();

            }
            else
            {
                return AdjectiveHolder.GetBadAdjective();
            }

        }
        public void ResetViewState()
        {
            OnDisable();
            WinningMessage.text = "";
            DebugBoardState.text = "";
            scrollBar.GetComponent<ScrollBarController>().Reset();
        }
        public void SetDebugBoardStateViews()
        {
            DebugBoardStateView = DebugBoardStateToggle.isOn;
        }
        public void SetMoveListView()
        {
            MoveListView = MoveListViewToggle.isOn;
        }

        public void DisplayForfeitMessage(GameResult result)
        {
            WinningMessage.text = $"{result.Winner} was playing the fool and as such they have forfeited the game.";
        }

        public void UpdateMoveList(MoveEvent moveEvent)
        {
            if (MoveListView)
            {
                DisplayMoveList(moveEvent);
            }
        }
        private void OnDisable()
        {
            if (MovesParent)
            {
                Transform[] children = MovesParent.GetComponentsInChildren<Transform>();
                for (int i = 1; i < children.Length; i++)
                {
                    if (children[i])
                    {
                        Destroy(children[i].gameObject);
                    }
                }
            }
        }

        public void DisplayDrawMessage()
        {
            WinningMessage.text = "Draw!!";
        }
    }
}