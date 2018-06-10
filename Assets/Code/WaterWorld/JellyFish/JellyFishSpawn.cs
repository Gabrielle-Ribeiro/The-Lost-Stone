using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishSpawn : MonoBehaviour {

	public float spawningHate = 20f;
	public float spawnCoolDown = 0f;
	public GameObject jellyFish;
	public Transform jellyFishSpawn;

	void Start () {
		
	}

	void Update () {
		if(spawnCoolDown > 0f){
			spawnCoolDown -= Time.deltaTime;
		}
		else if(spawnCoolDown <= 0){
			JellyFishSpawning ();
		}	
	}

	void JellyFishSpawning(){
		if(spawnCoolDown <= 0f){
			var cloneJellyFish = Instantiate(jellyFish, jellyFishSpawn.position, Quaternion.identity) as GameObject;
			spawnCoolDown = spawningHate;
		}
	}
}
