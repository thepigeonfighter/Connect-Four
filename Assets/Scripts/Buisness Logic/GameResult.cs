using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConnectFour;
public class GameResult {

	public GameStatus GameStatus { get; set; }
	public TeamName Winner { get; set; }
	public WinType WinType { get; set; }
	public List<BoardPosition> WinningPositions { get; set; }
	public IPlayer Player{ get; set; }
}
