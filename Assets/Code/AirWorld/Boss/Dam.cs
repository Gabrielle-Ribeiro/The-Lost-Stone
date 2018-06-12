using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dam : MonoBehaviour {

    public int life;
    public int playerDamage;

    // Use this for initialization
    void Start () {
    }
	
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            life -= playerDamage;
            if (life < 1)
            {
                DestroyObject(gameObject, 0);
            }

        }
        
    }
}
