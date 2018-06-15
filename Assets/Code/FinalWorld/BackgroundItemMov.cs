using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundItemMov : MonoBehaviour {


	public Vector2 speed = new Vector2 (-4.5f, 0);
	public Rigidbody2D itemRigidbody;

	void Start () {
		itemRigidbody = GetComponent<Rigidbody2D> ();
		itemRigidbody.velocity = speed * this.transform.localScale.x;
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.CompareTag("LimitLeft")){
			Destroy (this.gameObject);
		}
	}
}
