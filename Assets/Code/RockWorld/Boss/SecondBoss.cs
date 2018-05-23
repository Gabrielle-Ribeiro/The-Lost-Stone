using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBoss : MonoBehaviour {

	public Rigidbody2D bossRigidbody;

	// Variáveis que controlam a movimentação do Boss
	public float speed = 2.5f;
	public int direction = -1;
	public bool canWalk = true;


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

		if(bossIsAlive && canWalk){
			bossRigidbody.velocity = new Vector2 (speed * direction, bossRigidbody.velocity.y);
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		//Quando o jogador se aproxima do Boss, a movimentação do Boss é parada e o ataque é ativado
		if(collision.gameObject.CompareTag("Player")){
			canWalk = false;
			//AnimController.SetBool("isAttacking", true);
		}
	}

	void OnTriggerExit2D(Collider2D collision){
		// Quando o Boss sai do espaço delimitado de sua movimentação a sua direção é alterada
		if(collision.CompareTag("BossLimit") && bossIsAlive){
			direction = -direction;
		}
		// Quando o jogador se afasta do Boss, o Boss volta a se movimentar
		if(collision.gameObject.CompareTag("Player")){
			//AnimController.SetBool("isAttacking", false);
			canWalk = true;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		// Quando um tiro atinge o Boss, ele recebe 0.5 de dano e o GameObject Shot é detruído
		if(collision.gameObject.CompareTag("Shot")){
			bossLife -= 0.5f;
			Destroy (collision.gameObject);
		}
	}

	// Morte do Boss
	void BossDead(){
		bossIsAlive = false;
		Destroy (this.gameObject);
	}

}
