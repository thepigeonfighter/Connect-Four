using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ConnectFour
{
    [RequireComponent(typeof(AI_Base))]
    public class AI_Random : MonoBehaviour, IBrain
    {
        public Move GetDesiredMove(GameState gameState)
        {
            return ChooseRandomMove(gameState);
        }

        private Move ChooseRandomMove(GameState gameState)
        {
            List<ColumnIndex> availableColumns = gameState.AvailableColumns;
            int index = UnityEngine.Random.Range(0, availableColumns.Count);
            Move move = new Move()
            {
                Column = availableColumns[index]
            };
            return move;
        }


    }
}
