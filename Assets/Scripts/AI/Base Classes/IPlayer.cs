using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IPlayer
{
    EventHandler<MoveEvent> OnMoveComplete { get; set; }
    string GetName();
    void SignUp(GUID securityKey);
    void SetTeam(TeamName teamName);
    void OnTurnRequested(GameState gameState);
}
