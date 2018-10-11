using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBrain
{
    void SetTeam(TeamName teamName);
    Move GetDesiredMove( GameState gameState);

}
