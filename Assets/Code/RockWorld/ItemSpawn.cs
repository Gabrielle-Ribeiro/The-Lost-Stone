using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour {


	public Transform spawn; // GameObject Empty que servirá como local de criação dos itens 
	public int item; // Recebe um valor gerado aleatoriamente
	public GameObject batery; // valores aleatórios 1, 2, 3
	public GameObject life; // valor aleatório 4
	public bool canSpawnItem = true; // Controle para o item ser spawnado só uma vez

	void OnCollisionEnter2D(Collision2D coll){
		/* Se o Player colidir com uma plataforma do tipo ItemSpawn e canSpawnItem for true um item
		 * aleatório é spawnado
		 */
		if(coll.gameObject.CompareTag("Player") && canSpawnItem){
			RandomSpawn ();
			// Se o número gerado for 4 um item life é spawnado
			if(item == 4){
				var cloneShot = Instantiate(life, spawn.position, Quaternion.identity) as GameObject;
				canSpawnItem = false;
			}
			// Se os números 1, 2 ou 3 for gerado um item batery é spawnado
			else{
				var cloneShot = Instantiate(batery, spawn.position, Quaternion.identity) as GameObject;
				canSpawnItem = false;
			}
		}
	}

	// Método de geração aleatória de números 
	void RandomSpawn(){
		item = Random.Range (1, 5);
	}

}
