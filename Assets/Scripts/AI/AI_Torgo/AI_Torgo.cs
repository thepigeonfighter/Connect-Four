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
        private List<Target> _targets = new List<Target>();
        private List<BoardPosition> _moves = new List<BoardPosition>();
        private Target _selectedTarget;
        private TeamName _myTeam;
        public ColumnIndex ChooseColumnIndex(GameState gameState)
        {
            InitGameState(gameState);
            _targets = FindAvailableTargets(GetAvailableMoves());
            PickBestTarget();
            ColumnIndex index = PickRandomMoveBaseOnBestTarget();
            UpdateMovesList(index);
            return index;
        }
        private ColumnIndex PickRandomMoveBaseOnBestTarget()
        {
            if (_selectedTarget != null)
            {

                int requiredMovesCount = _selectedTarget.MovesRequiredToFillPath.Count;
                if (requiredMovesCount > 0)
                {
                    Debug.Log(requiredMovesCount);
                    BoardPosition pos = _selectedTarget.MovesRequiredToFillPath[0];
                    return (ColumnIndex)pos.XIndex;
                }else
                {
                    return (ColumnIndex)_selectedTarget.GetNextPosition(_currentBoard).XIndex;
                }
            }
            else{
                return ColumnIndex.Three;
            }
        }
        private void PickBestTarget()
        {
            Target bestTarget = new Target();
            OptionBuilder builder = new OptionBuilder(_myTeam);
            List<Option> options = new List<Option>();
            _moves.ForEach(x => options.Add(builder.BuildOption(x, _currentBoard, _targets)));
            _targets.Clear();
            foreach(Option o in options)
            {
                foreach(Target t in o.Targets)
                {
                    _targets.Add(t);
                }
            }
            _targets = _targets.OrderBy(x => x.GetFourCost(_currentBoard, _myTeam)).ToList();
            if (_targets.Count > 0)
            {
                _selectedTarget = _targets[0];
            }
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
        private void UpdateMovesList(ColumnIndex chosenColumn)
        {
            for (int i = 0; i < 6; i++)
            {
                if(!_currentBoard[(int)chosenColumn,i].IsOccupied)
                {
                    _moves.Add(_currentBoard[(int)chosenColumn, i]);
                    break;
                }
            }
        }
        private void InitGameState(GameState gameState)
        {
            _currentBoard = gameState.CurrentBoardState;
          //  _availableColumns = gameState.AvailableColumns;
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
            OptionBuilder builder = new OptionBuilder(_myTeam);
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
            if (_moves != null)
            {

                OptionBuilder builder = new OptionBuilder(_myTeam);

                foreach (BoardPosition bp in _moves)
                {
                    Option option = builder.BuildOption(bp, _currentBoard, _targets);
                    foreach (Target t in option.Targets)
                    {
                        Gizmos.DrawCube(t.TargetPosition.Position, new Vector2(.25f, .25f));
                        Vector2 labelPos = new Vector2(t.TargetPosition.Position.x, t.TargetPosition.Position.y +1f);
                        GUIStyle style = new GUIStyle();
                        Handles.Label(labelPos, t.GetFourCost(_currentBoard, _myTeam).ToString(), style);
                        Gizmos.color = Color.gray;
                        t.MovesRequiredToFillPath.ForEach(x => Gizmos.DrawSphere(x.Position, .25f));
                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere(t.TargetPosition.Position, .25f);
                        Gizmos.color = Color.green;
                        t.Path.ForEach(x => Gizmos.DrawSphere(x.Position, .15f));
                    }
                }
            }
            if(_selectedTarget != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_selectedTarget.TargetPosition.Position, .25f);
            }


        }
        
        public void SetTeam(TeamName teamName)
        {
            _myTeam = teamName;
        }

        public void OnRoundCompletion()
        {
            _availableMoves.Clear();
            _currentBoard = null;
            _moves.Clear();
            _selectedTarget = null;
            _targets = null;
            
        }
    }
    
}
