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
        public GameObject explosion;
        public bool GameOver;
        public float TurnDelay = .5f;
        private float _lastMove = 0f;
        private IMoveManager _moveManager;
        private GameBoard _gameBoard;
        private List<GameObject> _explosions = new List<GameObject>();
        private IStatsView _stats;
        private bool _timeToMove;
        public int TurnCount;
        // Use this for initialization
        void Awake()
        {
            _moveManager = GetComponent<IMoveManager>();
            _gameBoard = GetComponent<GameBoard>();
            _stats = GetComponent<IStatsView>();
            _moveManager.OnTeamsRegisteredEvent += StartGame;
            _moveManager.OnReadyForNextMove += ReadyForNextMove;

        }
        void Update()
        {
            if (_timeToMove)
            {
                OrderNextMove();
            }
        }
        private void ReadyForNextMove(object sender, MoveEvent e)
        {
            _timeToMove = true;

        }

        private void StartGame(object sender, bool e)
        {
            if (e)
            {

                _timeToMove = true;
            }
        }

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
            _lastMove = Time.time;
            TurnCount++;
            CheckForWin();
            
        }
        void CheckForWin()
        {
            GameState gameState = _moveManager.GetCurrentGameState();
            _stats.UpdateDebugBoardState(gameState);
            GameResult result = gameState.CurrentBoardState.CheckWin();
            if (result.GameStatus == GameStatus.Completed)
            {
                GameOver = true;
                _stats.DisplayWinMessage(result, TurnCount);
                StartCoroutine(DisplayWinningPieces(_gameBoard.GetListofBoardPositionsInGameSpace(result.WinningPositions)));
            }
        }
        public void ClearBoard()
        {
            _gameBoard.ClearBoard();
            Invoke("ResetGame", 2f);
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

    }
}