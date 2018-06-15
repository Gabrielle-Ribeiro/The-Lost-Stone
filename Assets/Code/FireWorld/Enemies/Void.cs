using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour {

	// Variáveis de controle da vida do inimigo
	public bool voidIsAlive = true;
	public int life = 10;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(life <= 0){
			VoidDead ();
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(voidIsAlive){
			if(coll.gameObject.CompareTag("Player")){
				//VoidAtack ();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(voidIsAlive){
			if(coll.gameObject.CompareTag("Shot")){
				life -= 1;
			}
		}
	}

	void VoidAtack(){
		// Animação ataque
	}

	void VoidDead(){
		voidIsAlive = false;
		Destroy (this.gameObject);
	}
}
