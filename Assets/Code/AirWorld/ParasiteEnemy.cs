using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteEnemy : MonoBehaviour {

	// Variáveis que controlam a movimentação do inimigo
	public float speed = 1.5f;
	public int direction = -1;
	public Rigidbody2D enemyRigidbody;
	public Transform enemyTransform;

	// Variáveis que de controle da vida do inimigo
	public bool enemyIsAlive = true;
	public int enemyLife = 10;
    public AudioClip soundHorse;//ariável do som
    public AudioClip attackHorse;
	void Start () {
		// Possibilita a manipulação dos componentes rigidbody e transform do inimigo
		enemyRigidbody = GetComponent<Rigidbody2D> ();
		enemyTransform = GetComponent<Transform> ();

	}

	void Update () {

		// Se a vida chegar a 0, o método de morte do inimigo é chamado
		if(enemyLife <= 0){
			EnemyDead ();
		}

		// Se o inimigo está vivo, ele se movimenta para a esquerda e direita
		if(enemyIsAlive){
           
				enemyRigidbody.velocity = new Vector2 (speed * direction, enemyRigidbody.velocity.y);
          
        }
	}

	void OnTriggerEnter2D(Collider2D coll){
		// Se o player entrar na área de trigger do inimigo, o ataque é ativado
		if(coll.gameObject.CompareTag("Player")){
     
			EnemyAtack ();
            SoundManager.instance.PlaySingle(attackHorse);
            SoundManager.instance.PlaySingle(soundHorse);
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

	// Quando o inimigo muda de direção, a direção do seu sprite também é altearda
	void Flip(){
		Vector3 scale = enemyTransform.localScale;
		scale.x *= -1;
		enemyTransform.localScale = scale;
	}

	// Método de ataque
	void EnemyAtack(){

	}

	// Quando o inimigo morre, seu gameObject é destruído
	void EnemyDead (){
		enemyIsAlive = false;
		Destroy (gameObject);
	}
}