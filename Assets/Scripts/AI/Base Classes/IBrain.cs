using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBrain
{
    Move GetDesiredMove( GameState gameState);

}
