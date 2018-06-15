using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAgent : MonoBehaviour {

	// Variáveis de controle da vida
	public bool alienIsAlive = true;
	public int life = 6;

	// Variáveis de controle da movimentação
	public Rigidbody2D alienRigidbody;
	public Transform alienAgentTransform;
	public float speed = 2f;
	public Transform target;
	public bool seeTarget = false;
	public int direction = -1;

	// Variáveis de controle do ataque
	public float alienShotingRate = 0.1f;    // Tempo entre um tiro e outro
	public float alienShotCoolDown = 0f;
	//public Transform alienBarrel;            // GameObject Empty que cria a bala
	//public GameObject alienShot;             // GameObject da bala 

	void Start () {
		alienAgentTransform = GetComponent<Transform> ();	
		alienRigidbody = GetComponent<Rigidbody2D> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(life <= 0){
			AlienDead ();
		}

		if(alienIsAlive){
			if(seeTarget){
				if (target.position.x > transform.position.x && direction < 0)
				{
					Flip ();
				}
				else if (target.position.x < transform.position.x && direction > 0)
				{
					Flip ();
				}
			}
			else{
				alienRigidbody.velocity = new Vector2 (speed * direction, alienRigidbody.velocity.y);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(alienIsAlive){
			if(coll.gameObject.CompareTag("Player")){
				seeTarget = true;
				//AlienAtack ();
			}
			if(coll.CompareTag("EnemyArea")){
				Flip ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if(alienIsAlive){
			if(coll.gameObject.CompareTag("Player")){
				seeTarget = false;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.CompareTag("Shot")){
			life -= 1;
		}
	}

	void AlienAtack(){
		
	}

	void AlienDead(){
		alienIsAlive = false;
		// Animação da morte
		Destroy (this.gameObject);
	}

	void Flip(){
		Vector3 scale = alienAgentTransform.localScale;
		scale.x *= -1;
		alienAgentTransform.localScale = scale;
		direction = -direction;
	}
}
