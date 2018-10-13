using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace ConnectFour.AI.AI_Torgo
{

    public class OptionBuilder
    {

        public Option BuildOption(BoardPosition boardPosition, BoardPosition[,] _currentBoard, List<BoardPosition> previousTargets)
        {
            Option option = new Option();
            option.MyPosition = boardPosition;
            option.Targets = SetTargets(boardPosition, _currentBoard);
            option.AvailableTargets = option.Targets.Count;
            option.IsTarget = IsThisOptionAlreadyATarget(boardPosition, previousTargets);

            return option;
        }
        private List<Target> SetTargets(BoardPosition boardPosition, BoardPosition[,]_currentBoard)
        {
            TargetBuilder builder = new TargetBuilder(_currentBoard);
            List<Target> targets = new List<Target>();
            List<TargetNames> targetNames = GetTargetNamesForBoardPosition(boardPosition);
            foreach(TargetNames name in targetNames)
            {
                Target target = builder.BuildTarget(boardPosition, name);
                targets.Add(target);

            }
            return targets;

        }
        #region Target Name Lists
        private List<TargetNames> GetTargetNamesForBoardPosition(BoardPosition boardPosition)
        {
            List<TargetNames> targetNames = new List<TargetNames>();
            if (boardPosition.XIndex < 3)
            {
                targetNames = GetTargetNamesForLeftSide(boardPosition.YIndex);
            }
            else if (boardPosition.XIndex == 3)
            {   
                targetNames = GetTargetNamesOnCenter(boardPosition.YIndex);
            }
            else if (boardPosition.XIndex > 3)
            {
                targetNames = GetTargetNamesForRightSide(boardPosition.YIndex);
            }
            return targetNames;
        }
        private List<TargetNames> GetTargetNamesForLeftSide(int yIndex)
        {
            List<TargetNames> targetsNames = new List<TargetNames>();
            if (yIndex > 2)
            {

                targetsNames.Add(TargetNames.Target_South);
                targetsNames.Add(TargetNames.Target_SouthEast);
                targetsNames.Add(TargetNames.Target_East);

            }
            else
            {
                targetsNames.Add(TargetNames.Target_North);
                targetsNames.Add(TargetNames.Target_NorthEast);
                targetsNames.Add(TargetNames.Target_East);
            }
            return targetsNames;

        }
        private List<TargetNames> GetTargetNamesForRightSide(int yIndex)
        {
            List<TargetNames> targetsNames = new List<TargetNames>();
            if (yIndex > 2)
            {

                targetsNames.Add(TargetNames.Target_South);
                targetsNames.Add(TargetNames.Target_SouthWest);
                targetsNames.Add(TargetNames.Target_West);

            }
            else
            {
                targetsNames.Add(TargetNames.Target_North);
                targetsNames.Add(TargetNames.Target_NorthWest);
                targetsNames.Add(TargetNames.Target_West);
            }
            return targetsNames;

        }
        private List<TargetNames> GetTargetNamesOnCenter(int yIndex)
        {
            List<TargetNames> targetsNames = new List<TargetNames>();
            if (yIndex > 2)
            {

                targetsNames.Add(TargetNames.Target_South);
                targetsNames.Add(TargetNames.Target_SouthWest);
                targetsNames.Add(TargetNames.Target_SouthEast);
                targetsNames.Add(TargetNames.Target_East);
                targetsNames.Add(TargetNames.Target_West);

            }
            else
            {
                targetsNames.Add(TargetNames.Target_North);
                targetsNames.Add(TargetNames.Target_NorthWest);
                targetsNames.Add(TargetNames.Target_NorthEast);
                targetsNames.Add(TargetNames.Target_East);
                targetsNames.Add(TargetNames.Target_West);
            }
            return targetsNames;

        }
        #endregion

        private bool IsThisOptionAlreadyATarget(BoardPosition boardPosition, List<BoardPosition> targets)
        {
            try
            {
                BoardPosition target = targets.First(x => x.Position == boardPosition.Position);
                return true;
            }
            catch
            {
                return false;
            }
        }
      
    }

}
