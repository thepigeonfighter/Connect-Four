using ConnectFour;
using ConnectFour.AI;
using ConnectFour.AI.AI_Torgo;
using System.Collections.Generic;
using UnityEngine;
namespace ConnectFour.AI.AI_FoulWind
{
    public class BoardScorer
    {
        internal class UnbeatableSet
        {
            public BoardPosition A { get; set; }
            public BoardPosition B { get; set; }
            public BoardPosition C { get; set; }
        }
        private BoardPosition[,] _currentBoard;
        private TeamName _myTeam;
        private TeamName _enemy;
        public int ScoreBoard(BoardPosition[,] currentBoard, TeamName team)
        {
            _currentBoard = currentBoard;
            _myTeam = team;
            _enemy = team.GetOppositeTeam();
            int totalScore = CheckForWin();
            if (totalScore != 0) { return totalScore; }
            totalScore = OneMoveWinScore();
            if (totalScore != 0) { return totalScore; }
            int enemyScore = CalculateScore(currentBoard, _enemy);
            int myScore = CalculateScore(currentBoard, _myTeam);
            totalScore += myScore - enemyScore;
            if (Mathf.Abs(totalScore) > 100) { Debug.Log("Score greater than 100"); }
            return totalScore;
        }
        private int CheckForWin()
        {
            if (GameOverState(_myTeam))
            {
                return 100;
            }
            else if (GameOverState(_enemy))
            {
                return -100;
            }
            return 0;
        }
        private int OneMoveWinScore()
        {
            if (CheckForOneMoveWin(_myTeam))
            {
                return 95;
            }
            else if (CheckForOneMoveWin(_enemy))
            {
                return -95;
            }
            return 0;
        }
        private bool CheckForOneMoveWin(TeamName team)
        {
            return CheckForUnBeatableWin(team) == 95;
        }
        private bool GameOverState(TeamName team)
        {
            int currentWin = CheckForGameOver(team);
            return currentWin == 100;

        }
        private int CheckForUnBeatableWin(TeamName team)
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (_currentBoard[i, j].Owner == team && _currentBoard[i + 1, j].Owner == team && _currentBoard[i + 2, j].Owner == team)
                    {
                        UnbeatableSet set = new UnbeatableSet
                        {
                            A = _currentBoard[i, j],
                            B = _currentBoard[i + 1, j],
                            C = _currentBoard[i + 2, j]
                        };
                        if (IsValidSet(set))
                        {
                            return 95;
                        }
                    }
                }
            }
            return 0;
        }
        private bool IsValidSet(UnbeatableSet set)
        {
            int yIndex = set.A.YIndex;
            if (_currentBoard[set.A.XIndex - 1, yIndex].IsOccupied)
            {
                return false;
            }
            if (_currentBoard[set.C.XIndex + 1, yIndex].IsOccupied)
            {
                return false;
            }
            else
            {
                return IsSetReachableInSingleMove(set);
            }

        }
        private bool IsSetReachableInSingleMove(UnbeatableSet set)
        {
            BoardPosition left = _currentBoard[set.A.XIndex - 1, set.A.YIndex];
            BoardPosition right = _currentBoard[set.C.XIndex + 1, set.C.YIndex];
            int totalMoves = left.MovesRequiredToReachPosition(_currentBoard) + right.MovesRequiredToReachPosition(_currentBoard);
            return totalMoves == 2;
        }

        private int CheckForGameOver(TeamName team)
        {
            CheckForWin winChecker = new CheckForWin();
            GameResult result = winChecker.CheckWin(_currentBoard);
            if (result.GameStatus == GameStatus.Completed)
            {
                if (result.Winner == team)
                {
                    return 100;
                }
            }
            return 0;
        }
        private int CalculateScore(BoardPosition[,] boardState, TeamName teamName)
        {
            OptionBuilder optionBuilder = new OptionBuilder(teamName);
            List<BoardPosition> ownedBoardPositions = new List<BoardPosition>();
            int totalScore = 0;
            foreach (var bp in boardState)
            {
                if (bp.Owner == teamName)
                {
                    ownedBoardPositions.Add(bp);
                }
            }

            foreach (var pos in ownedBoardPositions)
            {
                Option option = optionBuilder.BuildOption(pos, boardState);
                foreach (var target in option.Targets)
                {
                    if (target.CheckIfTargetValid(boardState, teamName))
                    {
                        int score = target.FourChance(boardState, teamName);
                        if (score == 6 && IsBlockable(target))
                        {
                            score /= 4;
                        }
                        totalScore += score;
                    }
                }

            }
            return totalScore;
        }
        private bool IsBlockable(Target target)
        {
            BoardPosition pos = target.GetNextPosition(_currentBoard);
            return pos.MovesRequiredToReachPosition(_currentBoard) == 1;
        }

    }
}