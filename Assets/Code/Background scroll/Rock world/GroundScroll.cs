using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroll : MonoBehaviour {

	public float speed = 0.1f;
	public Renderer background;

	void Start () {

	}

	void Update () {

		if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
			Vector2 offset = new Vector2 (speed * Time.deltaTime, 0);
			background.material.mainTextureOffset += offset;
		}
		else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
			Vector2 offset = new Vector2 (-speed * Time.deltaTime, 0);
			background.material.mainTextureOffset += offset;
		}
	}

}
