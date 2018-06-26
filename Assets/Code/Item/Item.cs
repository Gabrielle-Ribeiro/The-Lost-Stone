using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public Rigidbody2D itemRigidbody;

	void Start() {
		itemRigidbody = GetComponent<Rigidbody2D> ();
	}

	// Quando o Player encosta em um item, o item desaparece 
	void OnTriggerEnter2D(Collider2D collision){
		if(collision.gameObject.CompareTag("Player")){
			Destroy (this.gameObject);
		}
	}
}
