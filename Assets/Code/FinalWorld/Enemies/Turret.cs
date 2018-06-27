using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	// Variáveis de controle da vida
	public bool turretIsAlive = true;
	public int life = 6;

	// Variáveis de controle do ataque
	public bool seePlayer = false;

	//Controle das animações
	public Animator turretAnim;

	void Start () {

		// Controle das animações
		turretAnim = GetComponent<Animator> ();

		turretAnim.SetBool ("IsOpen", false);
		turretAnim.SetBool ("SeePlayer", false);
		//turretAnim.SetBool ("IsFiring", false);
		//turretAnim.SetBool ("IsScannig", false);

	}
	
	// Update is called once per frame
	void Update () {
		if(life <= 0){
			TurretDead ();
		}
		if(seePlayer){
			turretAnim.SetBool ("SeePlayer", true);
		}
		else{
			turretAnim.SetBool ("SeePlayer", false);
			TurretClose ();
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
			}
		}
	}

	void TurretOpen(){
		if(turretIsAlive){
			turretAnim.SetBool ("IsOpen", true);
		}
	}

	void TurretClose(){
		if(turretIsAlive){
			turretAnim.SetBool ("IsOpen", false);
		}
	}

	void TurretDead(){
		turretIsAlive = false;
		// Animação da morte
	}
}
