using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaHorse : MonoBehaviour {

	// Veriáveis de controle da movimentação do inimigo
	public float speed = 3;
	public int direction = -1;
	public Rigidbody2D seaHorseRigidbody;
	public Transform seaHorseTransform;

	// Variáveis de controle da vida do inimigo
	public bool enemyIsAlive = true;
	public float enemyLife = 10f;

	void Start () {
		seaHorseRigidbody = GetComponent<Rigidbody2D> ();
		seaHorseTransform = GetComponent<Transform> ();
	}

	void Update () {
		// Se a vida do inimigo chegar a 0, o método de sua morte é chamado
		if(enemyLife <= 0){
			EnemyDead ();
		}

		if(enemyIsAlive){
			// Se o inimigo estiver vivo ele se movimenta para esquerda e direita
			// A movimentação no eixo Y está congelada para o inimigo se manter flutuando
			seaHorseRigidbody.velocity = new Vector2 (speed * direction, seaHorseRigidbody.velocity.y);
			seaHorseRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		// Se o player entrar na área de trigger do inimigo, o ataque é ativado
		if(coll.gameObject.CompareTag("Player")){
			EnemyAtack ();
		}
		// Se o inimigo chegar no limite de sua área de movimentação sua direção de movimentação é alterada
		if(coll.CompareTag("EnemyArea")){
			direction = -direction;
			Flip ();
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		// Se o inimigo receber um tiro ele leva dano
		if(coll.gameObject.CompareTag("Shot")){
			enemyLife -= 1;
		}
	}

	// Método de alteração da direção do inimigo
	void Flip(){
		Vector3 scale = seaHorseTransform.localScale;
		scale.x *= -1;
		seaHorseTransform.localScale = scale;
	}

	// Método de ataque do inimigo
	void EnemyAtack(){

	}

	// Método de morte do inimigo
	void EnemyDead(){
		enemyIsAlive = !enemyIsAlive;
		Destroy (this.gameObject);
	}
}
