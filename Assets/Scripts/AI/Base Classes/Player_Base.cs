using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Base : MonoBehaviour {

    public abstract string GetName();
    public abstract void MakeMove(ColumnNumber index);
    public abstract void SignUp(Guid securityKey);

}
