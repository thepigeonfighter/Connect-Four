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
        public bool LimitTurnTime = false;
        public int TurnTimeSeconds = 60;

        private bool _timeToMove;
        private ColumnIndex _choosenColumn;
        private GameState _gameState;

        public async Task<ColumnIndex> GetDesiredMoveAsync(GameState gameState)
        {
            _gameState = gameState;
            _timeToMove = true;
            DisplayColumnOptions();
            if(LimitTurnTime)
            {
                await WaitForLimitedTime();
            }
            else
            {
                await WaitForEver();
            }
            return _choosenColumn;
        }
        public async Task WaitForEver()
        {
            while (_timeToMove)
            {
                await Task.Delay(200);
            }

        }
        public async Task WaitForLimitedTime()
        {
            int tries = 0;
            int timeOut = TurnTimeSeconds * 5;
            while (_timeToMove && tries < timeOut)
            {
                tries++;
                await Task.Delay(200);
            }

        }
        //This needs to be public so the buttons can access this script
        public void SetCapturedMove(int columnIndex)
        {
            ColumnIndex index = (ColumnIndex)columnIndex;
                try
                {
                //A little silly but if the column is not available this will throw a null
                //which we handle to tell the player to pick a different column
                _gameState.AvailableMoves.First(x => x.XIndex == columnIndex);


                    _choosenColumn = index;
                    _timeToMove = false;
                    AvailableMoves.SetActive(false);
                }
                catch
                {
                    Debug.Log("Column is full pick another one");
                }
            


        }
        private void DisplayColumnOptions()
        {
            AvailableMoves.SetActive(true);
        }


    }
}