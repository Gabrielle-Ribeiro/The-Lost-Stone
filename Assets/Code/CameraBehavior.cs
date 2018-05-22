using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	// Variáveis usadas na movimentação da câmera
	public Transform Target; 
	public Vector2 Offset = new Vector2(0f, 0f);

	void Start () {
		// Pega as coordenadas do GameObject MainChar e guarda na variável Target
		Target = GameObject.Find ("MainChar").transform;
		Vector3 targetPosition = this.transform.position;
		targetPosition.x = Target.position.x + Offset.x;
		this.transform.position = targetPosition;
	}

	void Update () {
		// Faz com que a câmera siga o MainChar
		Vector3 targetPosition = this.transform.position;
		targetPosition.x = Target.position.x + Offset.x;
		this.transform.position = targetPosition;
	}
}
