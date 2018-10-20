using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ConnectFour
{
    //All AI classes must have an AI_Base component on their gameobject
    [RequireComponent(typeof(AI_Base))]

    public class AI_Random : MonoBehaviour, IBrain
    {
        private TeamName _myTeam;

        /*
        This "ChooseColumnIndex" method will be called when it is your AI's turn to move
        you will be given a GameState object which has which contains an array of BoardPositions
        These board positions have the required information inside of them that will let your AI 
        make informed decisions. It also has a list of available columns which just represents columns that
         are not full yet. 
        */
        public ColumnIndex ChooseColumnIndex(GameState gameState)
        {
            
            return ChooseRandomMove(gameState);
        }

        public void SetTeam(TeamName teamName)
        {
            _myTeam = teamName;
        }

        /*
   GetDesiredMove must return a "Move" Object. The only information you need to 
   encode in that object is which column you would like to place your piece in
   the base classes handle the rest. 
*/

        private ColumnIndex ChooseRandomMove(GameState gameState)

        {
            List<ColumnIndex> availableColumns = gameState.AvailableColumns;
            int index = UnityEngine.Random.Range(0, availableColumns.Count);
            ColumnIndex randomColumn = availableColumns[index];


            return randomColumn;
        }


    }
}
