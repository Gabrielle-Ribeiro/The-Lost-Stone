using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	// Variáveis que determinam os limites de movimentação da câmera nos eixos x e y
	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;

	// Variável usada para guardar a posição do personagem principal
	private Transform target;

	void Start () {
		// Determina que as propriedades do GameObject guardadas em target serão as do Player
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void LateUpdate () {
		// Faz com que a câmera siga a movimentação do Player dentro dos limites determinados nos eixos x e y
		transform.position = new Vector3 (Mathf.Clamp (target.position.x, xMin, xMax), Mathf.Clamp (target.position.y, yMin, yMax), transform.position.z);
	}
}
