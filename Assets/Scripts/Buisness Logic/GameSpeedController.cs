using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour {
	[Range(0,100)]
    public float speed = 1;
	void Update () {
        Time.timeScale = speed;

    }
}
