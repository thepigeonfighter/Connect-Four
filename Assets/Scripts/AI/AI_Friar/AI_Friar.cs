using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ConnectFour.AI.AI_Friar
{
    //All AI classes must have an AI_Base component on their gameobject
    [RequireComponent(typeof(AI_Base))]

    public class AI_Friar : MonoBehaviour, IBrain
    {
        #region Public vars
        public bool debugMode;
        public TeamName _myTeam { get; set; }
        public Personalities personality;

        public enum Personalities
        {
            Default,
            Aggressive,
            Defensive,
            Balanced,
            TargetFocused
        }
        #endregion
        #region Private vars
        private BoardPosition[,] currentBoardState;

        private int[,] movesToWin = new int[7, 6];
        private int[,] enemyMovesToWin = new int[7, 6];
        private int[,] targetCount = new int[7, 6];
        private int[,] canPreventBlock = new int[7, 6];
        private int[,] canBlockEnemyWin = new int[7, 6];

        private int[,] movesToWinWeight = new int[7, 6];
        private int[,] enemyMovesToWinWeight = new int[7, 6];
        private int[,] targetCountWeight = new int[7, 6];
        private int[,] canPreventBlockWeight = new int[7, 6];
        private int[,] canBlockEnemyWinWeight = new int[7, 6];
        private int[,] enemyGameEndingMoveWeight = new int[7, 6];
        private int[,] playerGameEndingMoveWeight = new int[7, 6];

        private int movesToWinWeightFactor;            //Number of moves a tile is away from a win
        private int enemyMovesToWinWeightFactor;       //Same as above, but for enemy
        private int targetCountWeightFactor;           //Number of targets available from a tile
        private int noTargetWeightFactor;              //True if tile has no targets
        private int canPreventBlockWeightFactor;       //true if a choosing tile would allow enemy to block you next turn
        private int canBlockEnemyWinWeightFactor;      //true if a choosing tile would allow enemy to block you next turn
        private int enemyGameEndingMoveWeightFactor;   //True if choosing tile would allow enemy a win next turn
        private int playerGameEndingMoveWeightFactor;  //True if choosing tile would allow player a win next turn 

        private int[,] adjustedWeight = new int[7, 6];

        #endregion

        public void Start()
        {
            var myAI_base = GetComponent<AI_Base>();
            myAI_base.SetName("Friar (" + personality.ToString() + ")");

            #region Personality presets
            switch (personality)
            {
                case Personalities.Default:
                    movesToWinWeightFactor = 1;
                    enemyMovesToWinWeightFactor = 25;
                    targetCountWeightFactor = 1;
                    noTargetWeightFactor = 0;
                    canPreventBlockWeightFactor = 26;
                    canBlockEnemyWinWeightFactor = 90;
                    enemyGameEndingMoveWeightFactor = 100;
                    playerGameEndingMoveWeightFactor = 110;
                    break;

                case Personalities.Aggressive:
                    movesToWinWeightFactor = 25;
                    enemyMovesToWinWeightFactor = 5;
                    targetCountWeightFactor = 3;
                    noTargetWeightFactor = 0;
                    canPreventBlockWeightFactor = 6;
                    canBlockEnemyWinWeightFactor = 80;
                    enemyGameEndingMoveWeightFactor = 100;
                    playerGameEndingMoveWeightFactor = 110;
                    break;

                case Personalities.Defensive: //Merged into default
                    movesToWinWeightFactor = 1;
                    enemyMovesToWinWeightFactor = 25;
                    targetCountWeightFactor = 1;
                    noTargetWeightFactor = 0;
                    canPreventBlockWeightFactor = 26;
                    canBlockEnemyWinWeightFactor = 90;
                    enemyGameEndingMoveWeightFactor = 100;
                    playerGameEndingMoveWeightFactor = 110;
                    break;

                case Personalities.Balanced:
                    movesToWinWeightFactor = 15;
                    enemyMovesToWinWeightFactor = 5;
                    targetCountWeightFactor = 1;
                    noTargetWeightFactor = 1;
                    canPreventBlockWeightFactor = 16;
                    canBlockEnemyWinWeightFactor = 90;
                    enemyGameEndingMoveWeightFactor = 100;
                    playerGameEndingMoveWeightFactor = 110;
                    break;

                case Personalities.TargetFocused:
                    movesToWinWeightFactor = 1;
                    enemyMovesToWinWeightFactor = 25;
                    targetCountWeightFactor = 3;
                    noTargetWeightFactor = 1;
                    canPreventBlockWeightFactor = 16;
                    canBlockEnemyWinWeightFactor = 90;
                    enemyGameEndingMoveWeightFactor = 100;
                    playerGameEndingMoveWeightFactor = 110;
                    break;
            }
            #endregion

            CheckPersonalityPreset();
        }

        //The smaller of enemyGameEndingMoveWeightFactor and playerGameEndingMoveWeightFactor should be higher than the
        //highest gross outcome of the other factors. Otherwise, AI may not always choose to take or block a game ending move
        void CheckPersonalityPreset()
        {
            var factors1 =
                (movesToWinWeightFactor * 3)
                + (enemyMovesToWinWeightFactor * 3)
                + (targetCountWeightFactor * 5)
                - (canPreventBlockWeightFactor * 1)
                - (canBlockEnemyWinWeightFactor * 1)
                - (noTargetWeightFactor * 1);

            var factors2 = enemyGameEndingMoveWeightFactor < playerGameEndingMoveWeightFactor ?
                enemyGameEndingMoveWeightFactor : playerGameEndingMoveWeightFactor;

            if (factors2 <= factors1)
                Debug.LogWarning("Warning: AI_Friar personality unbalanced.");
        }

        //Called when it's your turn
        public ColumnNumber ChooseColumnIndex(GameState gameState)
        {
            currentBoardState = gameState.CurrentBoardState;

            ResetWeights();

            var moveChecker = new CheckMovesToWin();
            SetWeight(movesToWin, moveChecker.GetMoves(gameState, _myTeam));
            SetWeight(enemyMovesToWin, moveChecker.GetMoves(gameState, GetEnemyTeamName()));

            var targetChecker = new CheckTargetCount();
            SetWeight(targetCount, targetChecker.GetTargets(gameState, _myTeam));

            var preventativeMoveChecker = new CheckPreventativeMoves();
            SetWeight(canPreventBlock, preventativeMoveChecker.GetMoves(gameState, movesToWin));
            SetWeight(canBlockEnemyWin, preventativeMoveChecker.GetMoves(gameState, enemyMovesToWin));

            CalculateWeight();

            return ChooseBestMove(gameState);
        }

        private void SetWeight(int[,] targetArray, int[,] toBeAdded)
        {
            for (var v = 0; v < 6; v++)
            {
                for (var h = 0; h < 7; h++)
                {
                    targetArray[h, v] = toBeAdded[h, v];
                }
            }
        }

        private void CalculateWeight()
        {
            for (var v = 0; v < 6; v++)
            {
                for (var h = 0; h < 7; h++)
                {
                    #region Multiply each weight by its weight factor
                    //Targets
                    switch (targetCount[h, v])
                    {
                        case 0: targetCountWeight[h, v] -= 1 * noTargetWeightFactor; break;
                        default: targetCountWeight[h, v] += targetCount[h, v] * targetCountWeightFactor; break;
                    }

                    //Player moves to win
                    switch (movesToWin[h, v])
                    {
                        case 1:
                            movesToWinWeight[h, v] += 3 * movesToWinWeightFactor;
                            playerGameEndingMoveWeight[h, v] = 1 * playerGameEndingMoveWeightFactor;
                            break;
                        case 2: movesToWinWeight[h, v] += 2 * movesToWinWeightFactor; break;
                        case 3: movesToWinWeight[h, v] += 1 * movesToWinWeightFactor; break;
                    }

                    //Enemy moves to win
                    switch (enemyMovesToWin[h, v])
                    {
                        case 1:
                            enemyMovesToWinWeight[h, v] += 3 * enemyMovesToWinWeightFactor;
                            enemyGameEndingMoveWeight[h, v] = 1 * enemyGameEndingMoveWeightFactor;
                            break;
                        case 2: enemyMovesToWinWeight[h, v] += 2 * enemyMovesToWinWeightFactor; break;
                        case 3: enemyMovesToWinWeight[h, v] += 1 * enemyMovesToWinWeightFactor; break;
                    }

                    //Block enemy win weight (penalty)
                    switch (canBlockEnemyWin[h, v])
                    {
                        case 1: canBlockEnemyWinWeight[h, v] -= 1 * canBlockEnemyWinWeightFactor; break;
                    }

                    //Player block preventing moves (penalty)
                    switch (canPreventBlock[h, v])
                    {
                        case 1: canPreventBlockWeight[h, v] -= 1 * canPreventBlockWeightFactor; break;
                    }
                    #endregion

                    adjustedWeight[h, v] += (
                          targetCountWeight[h, v]
                        + movesToWinWeight[h, v]
                        + enemyMovesToWinWeight[h, v]
                        + canBlockEnemyWinWeight[h, v]
                        + canPreventBlockWeight[h, v])
                        + playerGameEndingMoveWeight[h, v]
                        + enemyGameEndingMoveWeight[h, v];
                }
            }
        }

        private ColumnNumber ChooseBestMove(GameState gameState)
        {
            BoardPosition bestTile = gameState.AvailableMoves[0];

            foreach (var position in gameState.AvailableMoves)
            {
                if (adjustedWeight[position.XIndex, position.YIndex] > adjustedWeight[bestTile.XIndex, bestTile.YIndex])
                {
                    bestTile = position;
                }
                else if (adjustedWeight[position.XIndex, position.YIndex] == adjustedWeight[bestTile.XIndex, bestTile.YIndex])
                {
                    if (UnityEngine.Random.Range(0, 2) == 0)
                    {
                        bestTile = position;
                    }
                }
            }
            return bestTile.XIndex;
        }

        #region Utilities and other things I was too lazy to put in separate classes
        private void ResetWeights()
        {
            Array.Clear(adjustedWeight, 0, adjustedWeight.Length);
            Array.Clear(movesToWinWeight, 0, movesToWinWeight.Length);
            Array.Clear(enemyMovesToWinWeight, 0, enemyMovesToWinWeight.Length);
            Array.Clear(canBlockEnemyWinWeight, 0, canBlockEnemyWinWeight.Length);
            Array.Clear(canPreventBlockWeight, 0, canPreventBlockWeight.Length);
            Array.Clear(playerGameEndingMoveWeight, 0, playerGameEndingMoveWeight.Length);
            Array.Clear(enemyGameEndingMoveWeight, 0, enemyGameEndingMoveWeight.Length);
            Array.Clear(targetCountWeight, 0, targetCountWeight.Length);
        }

        public static void DebugMsg(string message)
        {
            var AI = FindObjectOfType<AI_Friar>();
            if (AI.debugMode)
                Debug.Log("FRIAR DEBUG MSG ==== " + message);
        }

        //Called at end of each round
        public void OnRoundCompletion()
        {

        }

        private void OnDrawGizmos()
        {
            #region Draw gizmos
            if (currentBoardState != null && debugMode == true)
            {
                Handles.color = Color.yellow;

                for (var i = 0; i < 6; i++)
                {
                    for (var j = 0; j < 7; j++)
                    {
                        Handles.Label(new Vector2(currentBoardState[j, i].Position.x - .3f, currentBoardState[j, i].Position.y + .3f),
                            "  AW:" + adjustedWeight[j, i]
                            + "\nPM:" + movesToWinWeight[j, i]
                            + " EM:" + enemyMovesToWinWeight[j, i]
                            + "\nBL:" + canBlockEnemyWinWeight[j, i]
                            + " PR:" + canPreventBlockWeight[j, i]
                            + "\nGE:" + playerGameEndingMoveWeight[j, i]
                            + " EE:" + enemyGameEndingMoveWeight[j, i]
                            + "\n  TC:" + targetCountWeight[j, i]);
                    }
                }
            }
            #endregion
        }

        public void SetTeam(TeamName teamName)
        {
            _myTeam = teamName;
        }

        public TeamName GetEnemyTeamName()
        {
            switch (_myTeam)
            {
                case TeamName.BlackTeam: return TeamName.RedTeam;
                case TeamName.RedTeam: return TeamName.BlackTeam;
                default: throw new InvalidOperationException("Attention: C# is stupid");
            }
        }
        #endregion
    }
}
