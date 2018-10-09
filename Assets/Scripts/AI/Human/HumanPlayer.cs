using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
namespace ConnectFour
{
    public class HumanPlayer : MonoBehaviour, IHuman
    {
        public GameObject AvailableMoves;
        private bool _timeToMove;
        private Move _capturedMove;
        private GameState _gameState;
        public void SetCapturedMove(int columnIndex)
        {
            ColumnIndex index = (ColumnIndex)columnIndex;
            try
            {
                ColumnIndex match = _gameState.AvailableColumns.First(x => x == index);

                Move move = new Move()
                {
                    Column = index
                };
                _capturedMove = move;
                _timeToMove = false;
                AvailableMoves.SetActive(false);
            }
			catch{
                Debug.Log("Column is full pick another one");
            }

        }
        private void DisplayColumnOptions()
        {
            AvailableMoves.SetActive(true);
        }
        public async Task<Move> GetDesiredMoveAsync(GameState gameState)
        {
            _gameState = gameState;
            _timeToMove = true;
            DisplayColumnOptions();
            int tries = 0;
            int timeOut = 1000;
            while (_timeToMove && tries < timeOut)
            {
                tries++;
                await Task.Delay(200);
            }
            return _capturedMove;

        }
    }
}