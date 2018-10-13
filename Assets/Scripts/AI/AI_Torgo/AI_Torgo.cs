using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace ConnectFour.AI.AI_Torgo
{
    public class AI_Torgo : MonoBehaviour, IBrain
    {

        private BoardPosition[,] _currentBoard;
        private List<ColumnIndex> _availableColumns;
        private List<BoardPosition> _availableMoves;
        private List<BoardPosition> _targets = new List<BoardPosition>();
        private List<BoardPosition> _moves = new List<BoardPosition>();
        private Target _selectedTarget;
        private TeamName _myTeam;
        public ColumnIndex ChooseColumnIndex(GameState gameState)
        {
            InitGameState(gameState);
            int index = PickBestTarget().GetNextPosition(_currentBoard).XIndex;
            return (ColumnIndex)index;
        }

        private Target PickBestTarget()
        {
            Target bestTarget = new Target();
            List<Target> possibleTargets = FindAvailableTargets(GetAvailableMoves());
            possibleTargets.OrderBy(x => x.GetFourCost(_currentBoard, _myTeam));
            bestTarget = possibleTargets[0];
            return bestTarget;
        }
        private ColumnIndex ChooseRandomMove(GameState gameState)
        {
            List<ColumnIndex> availableColumns = gameState.AvailableColumns;
            int index = UnityEngine.Random.Range(0, availableColumns.Count);
            ColumnIndex randomColumn = availableColumns[index];
            return randomColumn;
        }
        private BoardPosition GetBoardPosition(Move move)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!_currentBoard[(int)move.Column, i].IsOccupied)
                {
                    return _currentBoard[(int)move.Column, i];
                }
            }
            return null;
        }
        private void InitGameState(GameState gameState)
        {
            _currentBoard = gameState.CurrentBoardState;
            _availableColumns = gameState.AvailableColumns;
            _availableMoves = GetAvailableMoves();


        }
        private List<BoardPosition> GetAvailableMoves()
        {
            List<BoardPosition> moves = new List<BoardPosition>();
            for (int i = 0; i < 7; i++)
            {
                int counter = 0;
                int max = 5;
                while (_currentBoard[i, counter].IsOccupied && counter < max)
                {
                    counter++;
                }
                if (!_currentBoard[i, counter].IsOccupied)
                {
                    moves.Add(_currentBoard[i, counter]);
                }
            }
            return moves;
        }
        private List<Target> FindAvailableTargets(List<BoardPosition> availableMoves)
        {
            OptionBuilder builder = new OptionBuilder();
            List<Target> targets = new List<Target>();
            foreach(BoardPosition bp in availableMoves)
            {
                Option option = builder.BuildOption(bp, _currentBoard, _targets);
                targets.AddRange(option.Targets);
            }
            return targets;
        }


        void OnDrawGizmos()
        {

            if (_availableMoves != null)
            {
                OptionBuilder builder = new OptionBuilder();
                foreach (BoardPosition bp in _availableMoves)
                {
                    Option option = builder.BuildOption(bp, _currentBoard, _targets);
                    if (option.AvailableTargets < 1)
                    {
                        Gizmos.color = Color.red;
                    }
                    else if (option.AvailableTargets < 4)
                    {
                        Gizmos.color = Color.yellow;
                    }
                    else
                    {
                        Gizmos.color = Color.green;
                    }
                    Gizmos.DrawCube(bp.Position, new Vector2(.3f, .3f));
                    Handles.Label(bp.Position, option.TotalScore.ToString());
                }   
                _targets.Clear();
                foreach (BoardPosition bp in _moves)
                {
                    Option option = builder.BuildOption(bp, _currentBoard,_targets);
                           
                    foreach (Target t in option.Targets)
                    {

                        if (t.CheckIfTargetValid(_currentBoard, _myTeam))
                        {
                            Gizmos.color = Color.cyan;
                            Handles.Label(t.TargetPosition.Position, t.GetFourCost(_currentBoard, _myTeam).ToString());
                            Gizmos.DrawSphere(t.TargetPosition.Position, .25f);
                            
                            Gizmos.color = Color.yellow;
                            t.Path.ForEach(x => Gizmos.DrawSphere(x.Position, .1f));
                        }

                    }
                    // option.Targets.ForEach(x => _targets.Add(x));
                }
            }


        }

        public void SetTeam(TeamName teamName)
        {
            _myTeam = teamName;
        }
    }
    
    //There has got be  a better way of calculating t 4cost 

    #region Foolish Calculations

    public class OptionCalculations
    {
        private BoardPosition[,] _currentBoard;
        private TeamName _myTeam;
        public int CalculateFourCost(Option option, BoardPosition[,] currentBoard, TeamName myTeam)
        {
            int fourCost = 4;
            int x = option.MyPosition.XIndex;
            int y = option.MyPosition.YIndex;
            _currentBoard = currentBoard;
            _myTeam = myTeam;
            if(fourCost > PositionsOwnedToRight(x,y))
            {
                fourCost = PositionsOwnedToRight(x, y);
            }
            if(fourCost > PositionsOwnedToLeft(x,y))
            {
                fourCost = PositionsOwnedToLeft(x, y);
            }
            if(fourCost > PositionsOwnedAbove(x,y))
            {
                fourCost = PositionsOwnedAbove(x, y);
            }
            if(fourCost > PositionsOwnedBelow(x,y))
            {
                fourCost = PositionsOwnedBelow(x, y);
            }
            if(fourCost > PositionsUpLeft(x,y))
            {
                fourCost = PositionsUpLeft(x, y);
            }
            if(fourCost > PositionsUpRight(x,y))
            {
                fourCost = PositionsUpRight(x, y);
            }
            if(fourCost > PositionsDownLeft(x,y))
            {
                fourCost = PositionsDownLeft(x, y);
            }
            if(fourCost > PositionsDownRight(x,y))
            {
                fourCost = PositionsDownRight(x, y);
            }
            return fourCost;
        }
        private int PositionsOwnedToRight(int x, int y)
        {
            int tempFourCost = 4;
            for (int i = 1; i < 4; i++)
            {
                int xRight = x + i;
                if (xRight < 7)
                {
                    if (_currentBoard[xRight, y].Owner == _myTeam )
                    {
                        tempFourCost--;
                    }
                }
            }
            return tempFourCost;
        }
        private int PositionsOwnedToLeft(int x, int y)
        {
            int tempFourCost = 4;
            for (int i = 1; i < 4; i++)
            {
                int xLeft = x - i;
                if (xLeft > 0)
                {
                    if (_currentBoard[xLeft, y].Owner == _myTeam)
                    {
                        tempFourCost--;
                    }
                }
            }
            return tempFourCost;
        }
        private int PositionsOwnedAbove(int x, int y)
        {
            int tempFourCost = 4;
            for (int i = 1; i < 4; i++)
            {
                int yUp = y + i;
                if (yUp < 6)
                {
                    if (_currentBoard[x, yUp].Owner == _myTeam)
                    {
                        tempFourCost--;
                    }
                }
            }
            return tempFourCost;
        }
        private int PositionsOwnedBelow(int x, int y)
        {
            int tempFourCost = 4;
            for (int i = 1; i < 4; i++)
            {
                int yDown = y - i;
                if (yDown > 0)
                {
                    if (_currentBoard[x, yDown].Owner == _myTeam )
                    {
                        tempFourCost--;
                    }
                }
            }
            return tempFourCost;
        }
        private int PositionsUpLeft(int x, int y)
        {
            int tempFourCost = 4;
            for (int i = 1; i < 4; i++)
            {
                int yUp = y + i;
                int xLeft = x - i;
                if (yUp < 6 && xLeft > 0)
                {
                    if (_currentBoard[xLeft, yUp].Owner == _myTeam)
                    {
                        tempFourCost--;
                    }
                }
            }
            return tempFourCost;
        }
        private int PositionsUpRight(int x, int y)
        {
            int tempFourCost = 4;
            for (int i = 1; i < 4; i++)
            {
                int yUp = y + i;
                int xRight = x + i;
                if (yUp < 6 && xRight < 7)
                {
                    if (_currentBoard[xRight, yUp].Owner == _myTeam)
                    {
                        tempFourCost--;
                    }
                }
            }
            return tempFourCost;
        }
        private int PositionsDownLeft(int x, int y)
        {
            int tempFourCost = 4;
            for (int i = 1; i < 4; i++)
            {
                int yDown = y - i;
                int xLeft = x - i;
                if (yDown > 0 && xLeft > 0)
                {
                    if (_currentBoard[xLeft, yDown].Owner == _myTeam)
                    {
                        tempFourCost--;
                    }
                }
            }
            return tempFourCost;
        }
        private int PositionsDownRight(int x, int y)
        {
            int tempFourCost = 4;
            for (int i = 1; i < 4; i++)
            {
                int yDown = y - i;
                int xRight = x + i;
                if (yDown > 0 && xRight < 7)
                {
                    if (_currentBoard[xRight, yDown].Owner == _myTeam)
                    {
                        tempFourCost--;
                    }
                }
            }
            return tempFourCost;
        }

    }
    #endregion
    

   
}
