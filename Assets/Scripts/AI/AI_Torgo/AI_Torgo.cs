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
        private List<Target> _myTargets = new List<Target>();
        private List<Target> _enemyTargets = new List<Target>();
        private List<BoardPosition> _moves = new List<BoardPosition>();
        private List<BoardPosition> _enemyMoves = new List<BoardPosition>();
        private Target _selectedTarget;
        private Target _enemySelectedTarget;
        private TeamName _myTeam;
        private TeamName _enemyTeam;
        public ColumnNumber ChooseColumnIndex(GameState gameState)
        {
            InitGameState(gameState);
            UpdateEnemyMoves();
            PickMyBestTarget();
            PickEnemyBestTarget();
            ColumnNumber index = PickRandomMoveBaseOnBestTarget(gameState);
            UpdateMovesList(index);
            return index;
        }
        private ColumnNumber PickRandomMoveBaseOnBestTarget(GameState gameState)
        {
            if (_enemySelectedTarget != null)
            {
                if (_enemySelectedTarget.GetFourCost(_currentBoard,_enemyTeam) < 2 )
                {
                    if (_selectedTarget != null && _selectedTarget.GetFourCost(_currentBoard, _myTeam) > 1)
                    {
                        return _enemySelectedTarget.GetNextPosition(_currentBoard).XIndex;
                    }
                }
            }
            if (_selectedTarget != null)
            {

                int requiredMovesCount = _selectedTarget.MovesRequiredToFillPath.Count;
                if (requiredMovesCount > 0)
                {
                    BoardPosition pos = _selectedTarget.MovesRequiredToFillPath[0];
                    return pos.XIndex;
                }
                else
                {
                    try
                    {
                        ColumnNumber column = _selectedTarget.GetNextPosition(_currentBoard).XIndex;
                        if (column != null)
                        {
                            return column;
                        }
                    }
                    catch
                    {
                        //I am so good at coding
                    }
                   
                    
                    
                }
              
            }
            return ChooseRandomMove(gameState);
        }
        private ColumnNumber ChooseRandomMove(GameState gameState)

        {
            int index = UnityEngine.Random.Range(0, gameState.AvailableMoves.Count);
            ColumnNumber randomColumn = gameState.AvailableMoves[index].XIndex;


            return randomColumn;
        }

        private void PickEnemyBestTarget()
        {
            List<Option> options = BuildOptionList(_enemyMoves, _enemyTargets, _enemyTeam);
            _enemyTargets = BuildTargetLists(options, _enemyTeam);
            _enemySelectedTarget = BestTargetFromList(_enemyTargets,_enemyTeam);


        }
        private void PickMyBestTarget()
        {
            List<Option> options = BuildOptionList(_moves, _myTargets, _myTeam);
            _myTargets = BuildTargetLists(options, _myTeam);
            _selectedTarget = BestTargetFromList(_myTargets, _myTeam);

        }
        private List<Option> BuildOptionList(List<BoardPosition> moves, List<Target> oldTargets , TeamName teamName)
        {
            OptionBuilder builder = new OptionBuilder(teamName);
            List<Option> options = new List<Option>();
            moves.ForEach(x => options.Add(builder.BuildOption(x, _currentBoard, oldTargets)));
            return options;
        }
        private List<Target> BuildTargetLists(List<Option> options, TeamName teamName)
        {
            List<Target> targets = new List<Target>();
            foreach (Option o in options)
            {
                foreach (Target t in o.Targets)
                {
                    if (t.CheckIfTargetValid(_currentBoard, teamName))
                    {
                        targets.Add(t);
                    }
                }
            }
            return targets;
        }
        private Target BestTargetFromList(List<Target> targets, TeamName teamName)
        {
            Target selectedTarget = null;
            targets = targets.OrderBy(x => x.GetFourCost(_currentBoard, teamName)).ToList();
            if (targets.Count > 0)
            {
                selectedTarget = targets[0];
            }
            return selectedTarget;
        }


        private void UpdateMovesList(ColumnNumber chosenColumn)
        {
            for (int i = 0; i < 6; i++)
            {
                if (!_currentBoard[(int)chosenColumn, i].IsOccupied)
                {
                    _moves.Add(_currentBoard[(int)chosenColumn, i]);
                    break;
                }
            }
        }
        private void InitGameState(GameState gameState)
        {
            _currentBoard = gameState.CurrentBoardState;
        }
        private void UpdateEnemyMoves()
        {
            List<BoardPosition> enemyMoves = new List<BoardPosition>();
            foreach (BoardPosition bp in _currentBoard)
            {
                if (bp.Owner == _enemyTeam)
                {
                    enemyMoves.Add(bp);
                }
            }
            _enemyMoves = enemyMoves;


        }



#if UNITY_EDITOR

        void OnDrawGizmos()
        {

            if (_moves != null)
            {

                OptionBuilder builder = new OptionBuilder(_myTeam);

                foreach (BoardPosition bp in _moves)
                {
                    Option option = builder.BuildOption(bp, _currentBoard, _myTargets);
                    foreach (Target t in option.Targets)
                    {
                        Gizmos.DrawCube(t.TargetPosition.Position, new Vector2(.25f, .25f));
                        Vector2 labelPos = new Vector2(t.TargetPosition.Position.x, t.TargetPosition.Position.y + .75f);
                        GUIStyle style = new GUIStyle();
                        Handles.Label(labelPos, t.GetFourCost(_currentBoard, _myTeam).ToString(), style);
                        Gizmos.color = Color.gray;
                        t.MovesRequiredToFillPath.ForEach(x => Gizmos.DrawSphere(x.Position, .25f));
                        Gizmos.color = Color.green;
                        t.Path.ForEach(x => Gizmos.DrawSphere(x.Position, .15f));
                    }
                }
            }
            if (_selectedTarget != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(_selectedTarget.TargetPosition.Position, .25f);

            }
            if (_enemySelectedTarget != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(_enemySelectedTarget.TargetPosition.Position, .45f);
            }
            if (_enemyTargets != null && _enemyTargets.Count > 0)
            {
                Gizmos.color = Color.red;
                _enemyTargets.ForEach(x => Gizmos.DrawWireSphere(x.TargetPosition.Position, .25f));
            }


        } 
#endif

        public void SetTeam(TeamName teamName)
        {
            switch(teamName)
            {
                case TeamName.BlackTeam:
                    _myTeam = TeamName.BlackTeam;
                    _enemyTeam = TeamName.RedTeam;
                    break;
                case TeamName.RedTeam:
                    _myTeam = TeamName.RedTeam;
                    _enemyTeam = TeamName.BlackTeam;
                    break;
            }
        }

        public void OnRoundCompletion()
        {
            _currentBoard = null;
            _moves.Clear();
            _selectedTarget = null;
            _myTargets = null;
            _enemyMoves.Clear();
            _enemySelectedTarget = null;
            _enemyTargets = null;
        }
    }

}
