using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour {

	// Variáveis que controlam a movimentação do inimigo
	public float speed = 2.5f;
	public int direction = -1;
	public bool followPlayer = false;
	public Rigidbody2D enemyRigidbody;
	public Transform enemyTransform;
	public Transform target;
	public Transform birdHouse;
    public AudioClip birdsound; //variável do som
    public AudioClip attackbird;

	// Variáveis de controle da vida do inimigo
	public bool enemyIsAlive = true;
	public float enemyLife = 10f;

	void Start () {
		// Permitem manipular os componentes Rigidbody e Transform do inimigo
		enemyRigidbody = GetComponent<Rigidbody2D> ();
		enemyTransform = GetComponent<Transform> ();

		// Define jogador como alvo
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {

		// Se a vida do inimigo chegar a 0, o método de sua morte é chamado
		if(enemyLife <= 0){
			EnemyDead ();
		}

		// Se o inimigo estiver vivo, sua movimentação ocorre
		if(enemyIsAlive){
			
			enemyRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
			// Se o player estiver dentro da área de trigger o inimigo segue ele
			if(followPlayer){
				if (target.position.x > transform.position.x && direction < 0)
				{
                   
					Flip ();
					direction = -direction;
				}
				else if (target.position.x < transform.position.x && direction > 0)
				{
					Flip ();
					direction = -direction;
				}

				enemyTransform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
			}
			// Caso contrário, o inimigo só se movimenta para a esquerda e direita dentro de um determinado limite
			else{
				
				enemyRigidbody.velocity = new Vector2 (speed * direction, enemyRigidbody.velocity.y);
				/*if (birdHouse.position.x > transform.position.x && direction < 0)
				{
					Flip ();
					direction = -direction;
				}
				else if (birdHouse.position.x < transform.position.x && direction > 0)
				{
					Flip ();
					direction = -direction;
				}

				enemyTransform.position = Vector2.MoveTowards(birdHouse.position, transform.position, speed * Time.deltaTime);*/

			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		// Se o player entrar na área de trigger do inimigo, ele será perseguido
		if(coll.gameObject.CompareTag("Player")){
			followPlayer = true;
            SoundManager.instance.PlaySingle(birdsound);
        }
		// Se o inimigo chegar no limite de sua área de movimentação, sua diração é alterada
		if(coll.CompareTag("EnemyArea")){
			followPlayer = false; 
			direction = -direction;
			Flip ();
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		// Se o player sair da área de trigger, o inimigo para de segui-lo
		if(coll.gameObject.CompareTag("Player")){
			followPlayer = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		// Se o inimigo colidir com o player o ataque é ativado
		if(coll.gameObject.CompareTag("Player")){
			Atack ();
            SoundManager.instance.PlaySingle(attackbird);
		}
		// Se o inimigo receber um tiro, levará dano
		if(coll.gameObject.CompareTag("Shot")){
			enemyLife -= 1;
		}
	}

	// Controle da direção do sprite do inimigo
	void Flip(){
		Vector3 scale = enemyTransform.localScale;
		scale.x *= -1;
		enemyTransform.localScale = scale;
	}

	// Método de ataque
	void Atack(){

	}

	// Destruição do GameObject, caso esse seja morto
	void EnemyDead (){
		enemyIsAlive = false;
		Destroy (gameObject);
	}
}
