using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class Item : MonoBehaviour{
    public AudioClip itemSound; // audio do item    
    // Quando o Player encosta em um item, o item desaparece 
    void OnTriggerEnter2D(Collider2D collision){
     if (collision.gameObject.CompareTag("Player")){
            SoundManager.instance.PlaySingle(itemSound);
            Destroy (this.gameObject);
=======
public class Item : MonoBehaviour {

	public Rigidbody2D itemRigidbody;

	void Start() {
		itemRigidbody = GetComponent<Rigidbody2D> ();
	}

	// Quando o Player encosta em um item, o item desaparece 
	void OnTriggerEnter2D(Collider2D collision){
		if(collision.gameObject.CompareTag("Player")){
			Destroy (this.gameObject);
>>>>>>> 46e5e3c89c8fd684cf28de98d54e86e2ef95b103
		}
	}
}
