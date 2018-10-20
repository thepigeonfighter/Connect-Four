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


        public void OnRoundCompletion()
        {
            //This will Get Called at the end of each round
            //This is where you would reset any internal lists or stats that you are keeping
            //Track of so the next round starts with a clean slate
        }
       public void SetTeam(TeamName teamName)
        {
            _myTeam = teamName;
        }
        private ColumnIndex ChooseRandomMove(GameState gameState)

        {
            List<ColumnIndex> availableColumns = gameState.AvailableColumns;
            int index = UnityEngine.Random.Range(0, availableColumns.Count);
            ColumnIndex randomColumn = availableColumns[index];


            return randomColumn;
        }


    }
}
