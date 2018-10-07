using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Player_Base : MonoBehaviour {

    public abstract string GetName();
    public abstract void MakeMove(Move move);
    public abstract void SignUp(GUID securityKey);

}
