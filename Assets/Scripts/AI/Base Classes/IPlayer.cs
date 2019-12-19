using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    EventHandler<MoveEvent> OnMoveComplete { get; set; }
    string GetName();
    void SignUp(Guid securityKey);
    void SetTeam(TeamName teamName);
    void OnTurnRequested(GameState gameState);

    void OnRoundCompleted();
}
