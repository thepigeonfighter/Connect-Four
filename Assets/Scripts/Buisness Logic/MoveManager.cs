using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

namespace ConnectFour
{

    public class MoveManager : MonoBehaviour, IMoveManager
    {
        #region  Public Vars
        public TeamName CurrentTeam = TeamName.BlackTeam;
        public EventHandler<bool> OnTeamsRegisteredEvent { get; set; }
        public EventHandler<MoveEvent> OnReadyForNextMove { get; set; }

        public EventHandler<TeamName> OnGameForfeit{get;set;}
        #endregion

        #region  Private Vars
        private IPlayer teamBlack;
        private IPlayer teamRed;
        private GUID teamBlackId;
        private GUID teamRedId;
        private GameBoard _gameBoard;
        private PiecePlacer piecePlacer;
        #endregion

        #region  Player Registration
        public void RegisterPlayer(IPlayer player)
        {
            if (teamBlack == null)
            {
                player.SetTeam(TeamName.BlackTeam);
                teamBlack = player;
                player.OnMoveComplete += OnPlayerMoveCompleted;
                teamBlackId = GetNewGUID();
                player.SignUp(teamBlackId);
                print("Team Black is being played by " + player.GetName() + " with the security id of " + teamBlackId.ToString());
            }
            else if (teamRed == null)
            {
                player.SetTeam(TeamName.RedTeam);
                teamRed = player;
                teamRedId = GetNewGUID();
                player.SignUp(teamRedId);
                print("Team Red is being played by " + player.GetName() + " with the security id of " + teamRedId.ToString());
                player.OnMoveComplete += OnPlayerMoveCompleted;
                OnTeamsRegisteredEvent?.Invoke(this, true);
            }
            else
            {
                print("No more players allowed at this time!");
            }
        }
        private GUID GetValidGUID(TeamName name)
        {
            switch (name)
            {
                case TeamName.BlackTeam:
                    return teamBlackId;
                case TeamName.RedTeam:
                    return teamRedId;
                default:
                    return new GUID();
            }
        }
        private GUID GetNewGUID()
        {
            return GUID.Generate();
        }
        #endregion

        #region  Movement Calls and Checks
        //This will take in the move that the AI has chosen
        //It should be able to verify the move is valid
        public void OnPlayerMoveCompleted(object sender, MoveEvent moveEvent)
        {
            GUID expectedId = GetValidGUID(CurrentTeam);
            if (expectedId == moveEvent.MySecurityHandle)
            {
                if (ValidateMove(moveEvent))
                {
                    piecePlacer.SetPiece(moveEvent.MyMove);
                    _gameBoard.SetMovement(moveEvent.MyMove);
                    SetNextTeamAsCurrent();
                    OnReadyForNextMove?.Invoke(this, moveEvent);
                }
                else{
                    
                    OnGameForfeit?.Invoke(this, CurrentTeam);
                }
            }
            else
            {
                print("WARNING HACKERS TRYING TO HACKETY HACKETY HACK!");
            }

        }
        private bool ValidateMove(MoveEvent moveEvent)
        {
            try
            {
                GetCurrentGameState().AvailableMoves.First(x => x.XIndex == (int) moveEvent.MyMove.Column);
                return true;
            }
            catch{
                return false;
            }
        }
        public void OnRoundCompletion()
        {
            teamBlack.OnRoundCompleted();
            teamRed.OnRoundCompleted();
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
        #endregion
        
        #region  GameState Builder
        public GameState GetCurrentGameState()
        {
            var currentBoard = _gameBoard.GetCurrentBoard();
            var availableMoves = GetAvailableMoves(currentBoard);
            GameState gameState = new GameState()
            {
#pragma warning disable 0618
                AvailableColumns = _gameBoard.GetAvailableColumns(),
#pragma warning restore 0618
                CurrentBoardState = currentBoard,
                AvailableMoves = availableMoves
            };
            return gameState;
        }
        private List<BoardPosition> GetAvailableMoves(BoardPosition[,] currentBoard)
        {
            List<BoardPosition> availableMoves = new List<BoardPosition>();
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if(!currentBoard[i,j].IsOccupied)
                    {
                        availableMoves.Add(currentBoard[i, j]);
                        break;
                    }
                }
            }
            return availableMoves;
        }
        #endregion
       
        #region  Internal Buisness Methods
        void Start()
        {
            _gameBoard = GetComponent<GameBoard>();
            piecePlacer = GetComponent<PiecePlacer>();
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
        #endregion 
    }
}
