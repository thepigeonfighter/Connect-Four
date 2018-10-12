using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBrain
{
<<<<<<< HEAD
    void SetTeam(TeamName teamName);
    Move GetDesiredMove( GameState gameState);
=======
    ColumnIndex ChooseColumnIndex( GameState gameState);
>>>>>>> master

}
