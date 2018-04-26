using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScroll : MonoBehaviour {

	public float speed = 0.025f;
	public Renderer background;

	void Start () {

	}

	void Update () {
		Vector2 offset = new Vector2 (speed * Time.deltaTime, 0);
		background.material.mainTextureOffset += offset;
	}
}
