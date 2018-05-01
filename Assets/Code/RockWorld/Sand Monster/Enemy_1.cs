using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{

    // Velocidade do inimigo
    public float speed = 1.5f;

    // Direção inicial do inimigo
    Vector2 direction = Vector2.left;

    void FixedUpdate()
    {
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        transform.localScale = new Vector2(-1 * transform.localScale.x,
            transform.localScale.y);

        direction = new Vector2(-1 * direction.x, direction.y);

    }


    // Se o personagem atingir o vilão ele morre
    // caso contrário o personagem morre
    void OnCollisionEnter2D(Collision2D col)
    {
        //Se acertar uma bala no personagem principal
        if (col.gameObject.name == "ShotMainChar(Clone)")
        {
            GetComponent<Collider2D>().enabled = false;

            DestroyObject(gameObject, 3);
            DestroyObject(col.gameObject, 0);
        }

        //Se nosso inimigo acertar o personagem
        if (col.gameObject.name == "MainChar")
        {
            DestroyObject(gameObject, 0);
            //Personagem principal toma dano
        }

        if (col.gameObject.tag == "Enemy_1" || col.gameObject.name == "Enemy_2")
        {
            transform.localScale = new Vector2(-1 * transform.localScale.x,
            transform.localScale.y);

            direction = new Vector2(-1 * direction.x, direction.y);
        }
    }


}