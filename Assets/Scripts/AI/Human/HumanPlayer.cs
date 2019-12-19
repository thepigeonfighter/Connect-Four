using System;
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
        private ColumnNumber _choosenColumn;
        private GameState _gameState;
        private Action<ColumnNumber> _onTurnChosen;
        public void GetMove(GameState gameState,Action<ColumnNumber> callback)
        {
            _gameState = gameState;
            _timeToMove = true;
            DisplayColumnOptions();
            _onTurnChosen = callback;
            StopAllCoroutines();
            StartCoroutine(WaitForMove());
        }
        private IEnumerator WaitForMove()
        {
            while(_timeToMove)
            {
                yield return null;
            }
            _onTurnChosen?.Invoke(_choosenColumn);

        }

        //This needs to be public so the buttons can access this script
        public void SetCapturedMove(int columnIndex)
        {

                try
                {
                //A little silly but if the column is not available this will throw a null
                //which we handle to tell the player to pick a different column
                _gameState.AvailableMoves.First(x => x.XIndex == columnIndex);


                    _choosenColumn = columnIndex;
                    _timeToMove = false;
                    AvailableMoves.SetActive(false);
                }
                catch
                {
                    MessageBoard.Instance.ShowMessage("Column is full pick another one");
                }
            


        }
        private void DisplayColumnOptions()
        {
            AvailableMoves.SetActive(true);
        }


    }
}