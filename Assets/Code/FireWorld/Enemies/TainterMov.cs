using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TainterMov : MonoBehaviour {

	// Variáveis de controle da vida do inimigo
	public bool tainterIsAlive = true;
	public int life = 10;

	// Variáveis usadas na movimentação
	public float speed = 2f;
	public int direction = 1;
	public Rigidbody2D tainterRigidbody;
	public Transform tainterTransform;
	public bool seePlayer = false;

	void Start () {
		tainterRigidbody = GetComponent<Rigidbody2D> ();
		tainterTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(life <= 0){
			TainterDead ();
		}
		if(tainterIsAlive){
			if(seePlayer == false){
				tainterRigidbody.velocity = new Vector2 (speed * direction, tainterRigidbody.velocity.y); // Movimentação
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(tainterIsAlive){
			if(coll.gameObject.CompareTag("Player")){
				seePlayer = true;
				//TainterAtack ();
			}
			if(coll.CompareTag("EnemyArea")){
				Flip ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if(tainterIsAlive){
			if(coll.gameObject.CompareTag("Player")){
				seePlayer = false;
			}
		}
	}

	void Flip (){
		Vector3 scale = tainterTransform.localScale;
		scale.x *= -1;
		tainterTransform.localScale = scale;
		direction = -direction;
	}

	void TainterAtack() {
		if(tainterIsAlive){
			//Animação ataque
		}
	}

	// Morte do inimigo
	void TainterDead(){
		tainterIsAlive = false;
		Destroy (this.gameObject);
	}
}
