using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour {
	[Range(0,10)]
    public float speed = 1;
	void Update () {
        Time.timeScale = speed;

    }
}
