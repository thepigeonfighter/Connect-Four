﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IHuman{

    Task<Move> GetDesiredMoveAsync(GameState gameState);
}
