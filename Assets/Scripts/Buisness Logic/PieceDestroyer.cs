using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDestroyer : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collisionInfo)
	{
        Destroy(collisionInfo.gameObject);
	}
}
