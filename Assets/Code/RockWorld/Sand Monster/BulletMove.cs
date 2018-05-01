using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

    public Transform BulletPrefab;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter2D(Collision2D col)
    {
        //Se acertar uma bala no personagem principal
        if (col.gameObject.name == "ShotMainChar(Clone)")
        {
            DestroyObject(gameObject, 0);
            DestroyObject(col.gameObject, 0);
        }

        //Se acertar o personagem principal
        else if (col.gameObject.name == "MainChar")
        {
            DestroyObject(gameObject, 0);
            //MainChar toma dano
        }

        //Se acertar o chão, cria o monstro menor
        else if (col.gameObject.name == "Background_3")
        {
            DestroyObject(gameObject, 0);

            var BulletTransform = Instantiate(BulletPrefab) as Transform;
            BulletTransform.position = transform.position;
        }
    }
}
