using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ConnectFour
{
    [RequireComponent(typeof(IMoveManager))]
    public class GameManager : MonoBehaviour
    {
        #region Public Vars
        public GameObject explosion;
        public Toggle AutoPlayToggle;
        public Button ClearBoardButton;
        public float TurnDelay = .5f;
        public float TimeBetweenGames = 1;
        public int RoundCount;
        #endregion

        #region Private Vars
        private float _lastMove = 0f;
        private IMoveManager _moveManager;
        private GameBoard _gameBoard;
        private List<GameObject> _explosions = new List<GameObject>();
        private IStatsView _stats;
        private bool _timeToMove;
        private int TurnCount;
        private bool GameOver;

        private CheckForWin winChecker = new CheckForWin();
        #endregion

        #region  MonoBehaviour Methods
        private void Awake()
        {
            _moveManager = GetComponent<IMoveManager>();
            _gameBoard = GetComponent<GameBoard>();
            _stats = GetComponent<IStatsView>();
            _moveManager.OnTeamsRegisteredEvent += StartGame;
            _moveManager.OnReadyForNextMove += ReadyForNextMove;
            _moveManager.OnGameForfeit += OnGameForfeit;

        }

        private void OnGameForfeit(object sender, TeamName e)
        {
            GameResult result = new GameResult();
            //This is misleading in this case it is actually the team that forfeited the game
            result.Winner = e;
            result.WinType = WinType.Forfeit;
            _stats.DisplayForfeitMessage(result);
            ScoreKeeper.UpdateStats(result);
            HandleEndOfRound();

        }

        private void Update()
        {
            if (_timeToMove)
            {
                OrderNextMove();
            }
        }
        #endregion

        #region Events
        private void ReadyForNextMove(object sender, MoveEvent e)
        {
            CheckGameState();
            _stats.UpdateMoveList(e);
            _lastMove = Time.time;
            _timeToMove = true;
        }

        private void StartGame(object sender, bool e)
        {
            if (e)
            {               
                _timeToMove = true;
            }
        }

        #endregion

        #region  Movement Methods
        private void OrderNextMove()
        {

            if (Time.time > _lastMove + TurnDelay && !GameOver)
            {
                _timeToMove = false;
                ExecuteMoveCall();
            }
        }
        private void ExecuteMoveCall()
        {
            _moveManager.RequestMove();
            TurnCount++;


        }
        #endregion

        #region  Board State Management
        private void CheckForAutoPlay()
        {
            if (AutoPlayToggle.isOn)
            {
                Invoke("ClearBoard", TimeBetweenGames);
            }
            else
            {
                ClearBoardButton.gameObject.SetActive(true);
            }
        }
        private void CheckGameState()
        {
            GameState gameState = _moveManager.GetCurrentGameState();

            GameResult result = winChecker.CheckWin(gameState.CurrentBoardState);
            if (result.GameStatus == GameStatus.Completed)
            {
                _stats.DisplayWinMessage(result, TurnCount);
                StartCoroutine(DisplayWinningPieces(_gameBoard.GetListofBoardPositionsInGameSpace(result.WinningPositions)));
                ScoreKeeper.UpdateStats(result);
                HandleEndOfRound();
            }
            else if(gameState.AvailableMoves.Count == 0)
            {
                _stats.DisplayDrawMessage();
                HandleEndOfRound();
            }


        }
        private void HandleEndOfRound()
        {
            GameOver = true;
            _moveManager.OnRoundCompletion();
            CheckForAutoPlay();
            winChecker = new CheckForWin();
        }
        public void ClearBoard()
        {
            _gameBoard.ClearBoard();
            Invoke("ResetGame", 2.5f);
        }
        public void ResetGame()
        {
            if (TurnCount>0)
            {
                RoundCount++;
            }
            TurnCount = 0;
            _explosions.ForEach(x => Destroy(x));
            _explosions.Clear();
            _stats.ResetViewState();
            GameOver = false;
            _timeToMove = true;
        }
        public void ReturnToMenu()
        {
            SceneManager.LoadScene("Menu");
        }
        private IEnumerator DisplayWinningPieces(List<Vector2> vectors)
        {
            yield return new WaitForSeconds(1.75f);
            int counter = 0;
            while (counter < vectors.Count)
            {
                GameObject gb = Instantiate(explosion, vectors[counter], Quaternion.identity) as GameObject;
                _explosions.Add(gb);
                counter++;
                yield return new WaitForSeconds(.25f);
            }
        }
        #endregion

    }
}