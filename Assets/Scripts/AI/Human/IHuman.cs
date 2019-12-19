using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IHuman{
    void GetMove(GameState gameState, Action<ColumnNumber> callback);
}
