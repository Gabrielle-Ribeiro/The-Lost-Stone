using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroll : MonoBehaviour {

	// Declaração de variáveis
	public float speed = 0.5f;
	public Renderer background;

	void Start () {

	}

	void Update () {

		/* Loop do background do "chão"
		 * A movimentação ocorre de acordo com as teclas de movimento para direita 
		 * e esquerda apertadas pelo usuário
		 */
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
