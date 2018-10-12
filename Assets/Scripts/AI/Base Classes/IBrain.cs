using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IBrain
{
    ColumnIndex ChooseColumnIndex( GameState gameState);

}
