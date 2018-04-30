using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScroll : MonoBehaviour {

	// Declaração de variáveis
	public float speed = 0.025f;
	public Renderer background;

	void Start () {

	}

	void Update () {
		// Loop da movimentação do background no eixo x
		Vector2 offset = new Vector2 (speed * Time.deltaTime, 0);
		background.material.mainTextureOffset += offset;
	}
}
