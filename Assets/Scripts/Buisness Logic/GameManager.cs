using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
namespace ConnectFour
{
    [RequireComponent(typeof(IMoveManager))]
    public class GameManager : MonoBehaviour
    {
        #region Public Vars
        public GameObject explosion;
        public Toggle AutoPlayToggle;
        public float TurnDelay = .5f;
        public float TimeBetweenGames = 1;
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
        #endregion
       
        #region  MonoBehaviour Methods
        void Awake()
        {
            _moveManager = GetComponent<IMoveManager>();
            _gameBoard = GetComponent<GameBoard>();
            _stats = GetComponent<IStatsView>();
            _moveManager.OnTeamsRegisteredEvent += StartGame;
            _moveManager.OnReadyForNextMove += ReadyForNextMove;
            _gameBoard.OnBoardFullEvent += BoardFullEvent;

        }
        void Update()
        {
            if (_timeToMove)
            {
                OrderNextMove();
            }
        }
        #endregion
    
        #region Events
        private void BoardFullEvent(object sender, EventArgs e)
        {
            GameOver = true;
            CheckForAutoPlay();
        }
        private void ReadyForNextMove(object sender, MoveEvent e)
        {
            CheckGameState();
            _timeToMove = true;
            _lastMove = Time.time;
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
        }
        void CheckGameState()
        {
            GameState gameState = _moveManager.GetCurrentGameState();
            _stats.UpdateDebugBoardState(gameState);
            GameResult result = gameState.CurrentBoardState.CheckWin();
            if (result.GameStatus == GameStatus.Completed)
            {
                GameOver = true;
                _stats.DisplayWinMessage(result, TurnCount);
                StartCoroutine(DisplayWinningPieces(_gameBoard.GetListofBoardPositionsInGameSpace(result.WinningPositions)));
                ScoreKeeper.UpdateStats(result);
                CheckForAutoPlay();
            }
        }
        public void ClearBoard()
        {
            _gameBoard.ClearBoard();
            Invoke("ResetGame", 2.5f);
        }
        private void ResetGame()
        {
            TurnCount = 0;
            _explosions.ForEach(x => Destroy(x));
            _explosions.Clear();
            _stats.ResetViewState();
            GameOver = false;
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