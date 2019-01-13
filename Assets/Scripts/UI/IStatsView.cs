using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsView
{
	void UpdateDebugBoardState(GameState gameState);
    void UpdateMoveList(MoveEvent moveEvent);
    void DisplayWinMessage(GameResult result, int turnCount);
    void DisplayDrawMessage();
    void DisplayForfeitMessage(GameResult result);
    void ResetViewState();
}