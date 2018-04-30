using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMov : MonoBehaviour {

	// Variáveis usadas para movimentar o GameObject Shot
	public Vector2 speed = new Vector2 (3, 0);
	public Rigidbody2D rigidbodyShot;

	void Start () {

		/* Indica que com a variável rigidbodyShot será possível manipular os valores das 
		 * propriedades do componente Rigidbody 2D do objeto MainChar
		 */
		rigidbodyShot = GetComponent<Rigidbody2D> ();

		// Movimentação do GameObject Shot
		rigidbodyShot.velocity = speed * this.transform.localScale.x;

		/* Destruição do GameObject Shot após 8 segundos caso ele não colida com 
		 * nenhum inimigo
		 */
		Destroy (gameObject, 8f);
	}

	void Update () {
		
	}

}
