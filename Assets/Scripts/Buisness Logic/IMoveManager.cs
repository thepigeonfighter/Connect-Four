using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveManager
{
    void RequestMove();
    void OnPlayerMoveCompleted(object sender, MoveEvent moveEvent);
    void RegisterPlayer(IPlayer player);
    GameState GetCurrentGameState();
}

