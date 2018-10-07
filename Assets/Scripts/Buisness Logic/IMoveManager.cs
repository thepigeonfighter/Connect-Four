using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IMoveManager
{
    void RequestMove();
    void OnPlayerMoveCompleted(object sender, MoveEvent moveEvent);
    void RegisterPlayer(IPlayer player);
    GameState GetCurrentGameState();
    EventHandler<bool> OnTeamsRegisteredEvent{ get; set; }
    EventHandler<MoveEvent> OnReadyForNextMove{ get; set; }
}

