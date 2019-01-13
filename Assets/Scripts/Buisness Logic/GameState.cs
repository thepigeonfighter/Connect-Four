using System;
using System.Collections.Generic;

public class GameState
{
    public BoardPosition[,] CurrentBoardState{ get; set; }
    [Obsolete("This property has been deprecated. Use available moves instead.")]
    public List<ColumnNumber> AvailableColumns{ get; set; }
    public List<BoardPosition> AvailableMoves { get; set; }
}