using System;

public enum TeamName { Empty, RedTeam, BlackTeam }
[Obsolete("Column Index has been deprecated. Use ColumnNumber now.")]
public enum ColumnIndex { Zero, One, Two, Three, Four, Five, Six }
public enum WinType { Horizontal, Vertical, Diagonal, Forfeit }
public enum GameStatus { NotStarted, InProgress, Completed }
public class ColumnNumber
{
    public int Value { get; set; }
    public string Name { get; set; }
    public static readonly ColumnNumber Zero = new ColumnNumber { Name = "Zero", Value = 0 };
    public static readonly ColumnNumber One = new ColumnNumber { Name = "One", Value = 1 };
    public static readonly ColumnNumber Two = new ColumnNumber { Name = "Two", Value = 2 };
    public static readonly ColumnNumber Three = new ColumnNumber { Name = "Three", Value = 3 };
    public static readonly ColumnNumber Four = new ColumnNumber { Name = "Four", Value = 4 };
    public static readonly ColumnNumber Five = new ColumnNumber { Name = "Five", Value = 5 };
    public static readonly ColumnNumber Six = new ColumnNumber { Name = "Six", Value = 6 };
    public static implicit operator int(ColumnNumber columnIndex)
    {
        return columnIndex.Value;
    }
    public static implicit operator ColumnNumber(int num)
    {

        switch (num)
        {
            case 0:
                return Zero;
            case 1:
                return One;
            case 2:
                return Two;
            case 3:
                return Three;
            case 4:
                return Four;
            case 5:
                return Five;
            case 6:
                return Six;
            default:
                throw new System.Exception("Invalid Column Index Value");
        }

    }


}

