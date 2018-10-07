using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsView
{
	void UpdateDebugBoardState(GameState gameState);
    void UpdateMoveList(GameState gameState);
    void DisplayWinMessage(GameResult result, int turnCount);
    void ResetViewState();
}