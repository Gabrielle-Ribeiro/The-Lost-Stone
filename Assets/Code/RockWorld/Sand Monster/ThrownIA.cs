using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownIA : MonoBehaviour {
    public float speed;                 // Velocidade do arremessável no eixo X
   
    Rigidbody2D rgbd_Thrown;            // Permite controlar a velocidade do arremessável
    Rigidbody2D rgbd_Player;            // Permite identificar a posição do jogador

    void Start () {
        rgbd_Thrown = GetComponent<Rigidbody2D>(); 

        // Define a velocidade do arremessável no eixo X
        // Velocidade negativada para que o arremessável vá para a esquerda
        rgbd_Thrown.velocity = new Vector2(-speed, 0) * transform.localScale.x;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.name != "SandMonster")
            
        DestroyObject(gameObject);
       
    }
}
