using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMov : MonoBehaviour {

	public Rigidbody2D bossRigidbody;

	// Variáveis que controlam a movimentação do Boss
	public float speed = 0.5f;
	public int direction = -1;

	// Variáveis que de controle da vida do Boss
	public bool bossIsAlive = true;
	public float bossLife = 10f;

	void Start () {
		bossRigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if(bossLife == 0){
			BossDead ();
		}
		if(bossIsAlive){
			bossRigidbody.velocity = new Vector2 (speed * direction, bossRigidbody.velocity.y);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		// Quando um tiro atinge o Boss, ele recebe 0.5 de dano e o GameObject Shot é detruído
		if(other.gameObject.CompareTag("Shot")){
			bossLife -= 0.5f;
			Destroy (other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		// Quando o Boss sai do espaço delimitado de sua movimentação a sua direção é alterada
		if(coll.CompareTag("BossLimit") && bossIsAlive){
			direction = -direction;
		}
	}

	// Morte do Boss
	void BossDead(){
		bossIsAlive = false;
		Destroy (this.gameObject);
	}
}

