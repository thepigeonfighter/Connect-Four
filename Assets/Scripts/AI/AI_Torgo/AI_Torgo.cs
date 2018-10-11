using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace ConnectFour.AI.AI_Torgo
{
    public class AI_Torgo : MonoBehaviour, IBrain
    {

        private BoardPosition[,] _currentBoard;
        private List<ColumnIndex> _availableColumns;
        private List<BoardPosition> _availableMoves;
        private List<BoardPosition> _moves = new List<BoardPosition>();
        private TeamName _myTeam;
        public Move GetDesiredMove(GameState gameState)
        {
            InitGameState(gameState);

            Move move = ChooseRandomMove(gameState);
            BoardPosition boardPosition = GetBoardPosition(move);
            _moves.Add(boardPosition);
            return move;

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


        void OnDrawGizmos()
        {

            if (_availableMoves != null)
            {
                OptionBuilder builder = new OptionBuilder();
               // OptionCalculations calculations = new OptionCalculations();
                //List<BoardPosition> availableMoves = GetAvailableMoves();
                // _availableMoves.ForEach(x => Gizmos.DrawCube(x.Position, new Vector2(.3f, .3f)));
                foreach (BoardPosition bp in _availableMoves)
                {
                    Option option = builder.BuildOption(bp, _currentBoard);
                   // option.FourCost =  calculations.CalculateFourCost(option, _currentBoard, _myTeam);
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
                Gizmos.color = Color.cyan;
                foreach (BoardPosition bp in _moves)
                {
                    Option option = builder.BuildOption(bp, _currentBoard);
                    option.Targets.ForEach(x => Gizmos.DrawSphere(x.Position, .25f));
                }
            }
            

        }

        public void SetTeam(TeamName teamName)
        {
            _myTeam = teamName;
        }
    }
    /*
    There has got be  a better way of calculating t 4cost 



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
    */
    public class Option
    {
        public int TotalScore { get { return GetTotalScore(); } private set { TotalScore = value; } }
        public BoardPosition MyPosition { get; set; }
        public List<Vector2Int> TargetIndices { get; set; }
        public List<BoardPosition> Targets { get; set; }
        public int AvailableTargets { get; set; }
        public int FourCost { get; set; }
        public int EFourCost { get; set; }
        public int ConsequenceCost { get; set; }
        private int GetTotalScore()
        {
            return AvailableTargets - FourCost - EFourCost - ConsequenceCost;
        }
    }
    public class OptionBuilder
    {

        private enum TargetNames { Target_North, Target_NorthEast, Target_East, Target_SouthEast, Target_South, Target_SouthWest, Target_West, Target_NorthWest }
        private Dictionary<TargetNames, Vector2Int> _targetToCoordDictionary = new Dictionary<TargetNames, Vector2Int>();
        public OptionBuilder()
        {
            InitDictionary();
        }
        private void InitDictionary()
        {
            _targetToCoordDictionary.Add(TargetNames.Target_North, new Vector2Int(0, 3));
            _targetToCoordDictionary.Add(TargetNames.Target_NorthEast, new Vector2Int(3, 3));
            _targetToCoordDictionary.Add(TargetNames.Target_East, new Vector2Int(3, 0));
            _targetToCoordDictionary.Add(TargetNames.Target_SouthEast, new Vector2Int(3, -3));
            _targetToCoordDictionary.Add(TargetNames.Target_South, new Vector2Int(0, -3));
            _targetToCoordDictionary.Add(TargetNames.Target_SouthWest, new Vector2Int(-3, -3));
            _targetToCoordDictionary.Add(TargetNames.Target_West, new Vector2Int(-3, 0));
            _targetToCoordDictionary.Add(TargetNames.Target_NorthWest, new Vector2Int(-3, 3));
        }
        public Option BuildOption(BoardPosition boardPosition, BoardPosition[,] _currentBoard)
        {
            Option option = new Option();
            option.TargetIndices = GetAvailableTargets(boardPosition);
            option.MyPosition = boardPosition;
            option.AvailableTargets = option.TargetIndices.Count;
            option.Targets = BuildTargetList(option.TargetIndices, boardPosition, _currentBoard);
            return option;
        }
        private List<BoardPosition> BuildTargetList(List<Vector2Int> targetIndices, BoardPosition boardPosition, BoardPosition[,] curentBoard)
        {
            List<BoardPosition> targets = new List<BoardPosition>();
            // targetIndices.ForEach(x => Debug.Log(x));
            foreach (Vector2Int v in targetIndices)
            {
                int newX = boardPosition.XIndex + v.x;
                int newY = boardPosition.YIndex + v.y;
                if (!curentBoard[newX, newY].IsOccupied)
                {

                    //Debug.Log("Board Index was "+ boardPosition.XIndex +" , " + boardPosition.YIndex +  "X= " + newX + ", Y = " + newY);
                    targets.Add(curentBoard[newX, newY]);

                }
            }
            return targets;
        }
        private List<Vector2Int> GetAvailableTargets(BoardPosition boardPosition)
        {
            List<Vector2Int> targets = new List<Vector2Int>();
            if (boardPosition.XIndex < 3)
            {
                targets = GetAvailableTargetsOnLeftSide(boardPosition.YIndex);
            }
            else if (boardPosition.XIndex == 3)
            {
                targets = GetAvailableTargetsOnCenter(boardPosition.YIndex);
            }
            else if (boardPosition.XIndex > 3)
            {
                targets = GetAvailableTargetsOnRightSide(boardPosition.YIndex);
            }
            return targets;
        }
        private List<Vector2Int> GetAvailableTargetsOnLeftSide(int y)
        {
            List<Vector2Int> targets = new List<Vector2Int>();
            if (y > 2)
            {
                targets = GetTopLeftTargetList();
            }
            else
            {
                targets = GetBottomLeftTargetList();
            }
            return targets;
        }
        private List<Vector2Int> GetAvailableTargetsOnRightSide(int y)
        {
            List<Vector2Int> targets = new List<Vector2Int>();
            if (y > 2)
            {
                targets = GetTopRightTargetList();
            }
            else
            {
                targets = GetBottomRightTargetList();
            }
            return targets;
        }
        private List<Vector2Int> GetAvailableTargetsOnCenter(int y)
        {
            List<Vector2Int> targets = new List<Vector2Int>();
            if (y > 2)
            {
                targets = GetTopCenterTargetList();
            }
            else
            {
                targets = GetBottomCenterTargetList();
            }
            return targets;
        }

        #region Target Lists
        private List<Vector2Int> GetBottomLeftTargetList()
        {
            List<Vector2Int> vectors = new List<Vector2Int>();
            Vector2Int north, northEast, east;
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_North, out north);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_NorthEast, out northEast);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_East, out east);
            vectors.Add(north);
            vectors.Add(northEast);
            vectors.Add(east);
            return vectors;
        }
        private List<Vector2Int> GetBottomRightTargetList()
        {
            List<Vector2Int> vectors = new List<Vector2Int>();
            Vector2Int north, northWest, west;
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_North, out north);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_NorthWest, out northWest);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_West, out west);
            vectors.Add(north);
            vectors.Add(northWest);
            vectors.Add(west);
            return vectors;
        }
        private List<Vector2Int> GetTopRightTargetList()
        {
            List<Vector2Int> vectors = new List<Vector2Int>();
            Vector2Int south, southWest, west;
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_South, out south);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_SouthWest, out southWest);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_West, out west);
            vectors.Add(south);
            vectors.Add(southWest);
            vectors.Add(west);
            return vectors;
        }
        private List<Vector2Int> GetTopLeftTargetList()
        {
            List<Vector2Int> vectors = new List<Vector2Int>();
            Vector2Int south, southEast, east;
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_South, out south);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_SouthEast, out southEast);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_East, out east);
            vectors.Add(south);
            vectors.Add(southEast);
            vectors.Add(east);
            return vectors;
        }
        private List<Vector2Int> GetTopCenterTargetList()
        {
            List<Vector2Int> vectors = new List<Vector2Int>();
            Vector2Int south, southEast, east, west, southWest;
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_South, out south);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_SouthEast, out southEast);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_East, out east);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_SouthWest, out southWest);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_West, out west);
            vectors.Add(south);
            vectors.Add(southEast);
            vectors.Add(east);
            vectors.Add(southWest);
            vectors.Add(west);
            return vectors;
        }
        private List<Vector2Int> GetBottomCenterTargetList()
        {
            List<Vector2Int> vectors = new List<Vector2Int>();
            Vector2Int north, northWest, west, east, northEast;
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_North, out north);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_NorthWest, out northWest);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_West, out west);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_NorthEast, out northEast);
            _targetToCoordDictionary.TryGetValue(TargetNames.Target_East, out east);
            vectors.Add(north);
            vectors.Add(northWest);
            vectors.Add(west);
            vectors.Add(northEast);
            vectors.Add(east);
            return vectors;
        }
        #endregion
    }
}
