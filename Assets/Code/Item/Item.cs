using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour{
    public AudioClip itemSound; // audio do item    
    // Quando o Player encosta em um item, o item desaparece 
    void OnTriggerEnter2D(Collider2D collision){
     if (collision.gameObject.CompareTag("Player")){
            SoundManager.instance.PlaySingle(itemSound);
            Destroy (this.gameObject);
		}
	}
}
