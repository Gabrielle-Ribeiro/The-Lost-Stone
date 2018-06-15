using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	// Variáveis de controle da vida
	public bool turretIsAlive = true;
	public int life = 6;

	// Variáveis de controle do ataque
	public bool seePlayer = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(life <= 0){
			TurretDead ();
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(turretIsAlive){
			if(coll.gameObject.CompareTag("Player")){
				seePlayer = true;
				TurretOpen ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if(turretIsAlive){
			if(coll.gameObject.CompareTag("Player")){
				seePlayer = false;
				TurretClose ();
			}
		}
	}

	void TurretOpen(){
		if(turretIsAlive){
			// Animação TurretOpen
		}
	}

	void TurretClose(){
		if(turretIsAlive){
			// Animação Turret Close
		}
	}

	void TurretIdle(){
		if(turretIsAlive && seePlayer){
			// Animação Turret Idle
			TurretAtack (); // No fim da animação turret idle
		}
	}

	void TurretAtack(){
		if(turretIsAlive && seePlayer){
			// Animação TurretAtack
		}
	}

	void TurretDead(){
		turretIsAlive = false;
		// Animação da morte
	}
}
