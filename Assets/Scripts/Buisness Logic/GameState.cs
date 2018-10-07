using System.Collections.Generic;

public class GameState
{
    public BoardPosition[,] CurrentBoardState{ get; set; }
    public List<ColumnIndex> AvailableColumns{ get; set; }
}