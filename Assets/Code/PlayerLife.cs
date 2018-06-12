using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {

	public float vidas;

	// Use this for initialization
	void CollisionEnter2D (Collision2D col) {

		if (col.transform.tag == "DANO") {
			vidas -= 5;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
