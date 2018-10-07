using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ConnectFour
{

    public class MoveManager : MonoBehaviour, IMoveManager
    {
        public TeamName CurrentTeam = TeamName.BlackTeam;
        private IPlayer teamBlack;
        private IPlayer teamRed;
        private GUID teamBlackId;
        private GUID teamRedId;
        private GameBoard _gameBoard;
        private PiecePlacer piecePlacer;
        private bool _gameReadyToStart;
        // Use this for initialization
        void Start()
        {
            _gameBoard = GetComponent<GameBoard>();
            piecePlacer = GetComponent<PiecePlacer>();
        }
        public void RegisterPlayer(IPlayer player)
        {
            if (teamBlack == null)
            {
                player.SetTeam(TeamName.BlackTeam);
                teamBlack = player;
                player.OnMoveComplete += OnPlayerMoveCompleted;
                teamBlackId = GetNewGUID();
                player.SignUp(teamBlackId);
                print("Team Black is being played by " + player.GetName() + "with the security id of " + teamBlackId.ToString());
            }
            else if (teamRed == null)
            {
                player.SetTeam(TeamName.RedTeam);
                teamRed = player;
                teamRedId = GetNewGUID();
                player.SignUp(teamRedId);
                print("Team Red is being played by " + player.GetName() + "with the security id of " + teamRedId.ToString());
                player.OnMoveComplete += OnPlayerMoveCompleted;
                _gameReadyToStart = true;
            }
            else
            {
                print("No more players allowed at this time!");
            }
        }

        //This will take in the move that the AI has chosen
        //It should be able to verify the move is valid
        public void OnPlayerMoveCompleted(object sender, MoveEvent moveEvent)
        {
            //TODO Check to make sure security handle is valid
            piecePlacer.SetPiece(moveEvent.MyMove);
            _gameBoard.SetMovement(moveEvent.MyMove);
            SetNextTeamAsCurrent();
        }
        private GUID GetNewGUID()
        {
            return GUID.Generate();
        }
        //This should tell the current player that they need to move
        public void RequestMove()
        {
            switch (CurrentTeam)
            {
                case TeamName.BlackTeam:
                    teamBlack.OnTurnRequested(GetCurrentGameState());
                    break;
                case TeamName.RedTeam:
                    teamRed.OnTurnRequested(GetCurrentGameState());
                    break;
            }
        }
        public GameState GetCurrentGameState()
        {
            GameState gameState = new GameState()
            {
                AvailableColumns = _gameBoard.GetAvailableColumns(),
                CurrentBoardState = _gameBoard.GetCurrentBoard()
            };
            return gameState;
        }

        private void SetNextTeamAsCurrent()
        {
            switch (CurrentTeam)
            {
                case TeamName.BlackTeam:
                    CurrentTeam = TeamName.RedTeam;
                    break;
                case TeamName.RedTeam:
                    CurrentTeam = TeamName.BlackTeam;
                    break;
            }
        }
    }
}
