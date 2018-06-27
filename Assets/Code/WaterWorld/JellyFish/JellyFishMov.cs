using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishMov : MonoBehaviour {

	// Variáveis usadas na movimentação
	public int direction = -1;
	public float speed = 1.5f;
	public Rigidbody2D jellyFishRigidbody;
	public Transform jellyFishTransform;
    // variável do som 
    public AudioClip soundfish;
	void Start () {
		jellyFishRigidbody = GetComponent<Rigidbody2D> ();
		jellyFishTransform = GetComponent<Transform> ();
	}

	void Update () {
		jellyFishRigidbody.velocity = new Vector2 (speed * direction, jellyFishRigidbody.velocity.y);
        Destroy (this.gameObject, 8f);
       
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.CompareTag("EnemyArea")){
			direction = -direction;
            Flip ();
            SoundManager.instance.PlaySingle(soundfish);

        }
	}

	void Flip(){
		Vector3 scale = jellyFishTransform.localScale;
		scale.x *= -1;
		jellyFishTransform.localScale = scale;
	}
}
