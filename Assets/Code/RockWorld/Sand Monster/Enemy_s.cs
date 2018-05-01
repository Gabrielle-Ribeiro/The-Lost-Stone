using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_s : MonoBehaviour {


    // Velocidade do inimigo
    public float speed = 0.0f;

    public int life;

    //public Sprite Max;
    //public Sprite Medium;

    // Direção inicial do inimigo
    Vector2 direction = Vector2.left;

    void Start()
    {
        life = 3;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;

    }

    // Se o personagem atingir o vilão ele morre
    // caso contrário o personagem morre
    void OnCollisionEnter2D(Collision2D col)
    {
        //Se a bala acertar nosso inimigo
        if (col.gameObject.name == "ShotMainChar(Clone)")
        {
            if (life == 1)
            {
                DestroyObject(gameObject, 0);
            }
            
            //else if (life > 1)
                //Particula de dano (TakenDamage)

            DestroyObject(col.gameObject, 0);
            life--;
        }

        //Se nosso inimigo acertar o personagem
        if (col.gameObject.name == "MainChar")
        {
            //Causar Dano no Personagem Principal
        }
    }


}