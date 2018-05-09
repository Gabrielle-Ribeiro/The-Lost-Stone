using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownIA : MonoBehaviour {
    public float speed;                 // Velocidade do arremessável no eixo X

    public Transform sandMonsterMinor;  // Prefab do npc a ser criado quando houver uma colisão

    Rigidbody2D rgbd_Thrown;            // Permite controlar a velocidade do arremessável

    void Start () {
        rgbd_Thrown = GetComponent<Rigidbody2D>(); 

        // Define a velocidade do arremessável no eixo X
        // Velocidade negativada para que o arremessável vá para a esquerda
        rgbd_Thrown.velocity = new Vector2(-speed, 0) * transform.localScale.x;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Destroi o arremessável se o mesmo for atingido por uma bala
        if (col.gameObject.CompareTag("Shot"))
        {
            DestroyObject(gameObject, 0);
        }

        // Destroi o arremessável se o mesmo atingir um jogador
        else if (col.gameObject.CompareTag("Player"))
            DestroyObject(gameObject, 0);

        // Cria um Sand Monster Minor caso atinja qualquer outra coisa
        // FIX: limitação implementada para que o arremessável não crie um Minor assim que for gerado
        // o arremessável colide com o Sand Monster ao ser gerado
        else if (!col.gameObject.CompareTag("SandMonster"))
        {
            DestroyObject(gameObject, 0);

            var BulletTransform = Instantiate(sandMonsterMinor) as Transform;   // Instancia um Minor
            BulletTransform.position = transform.position;                      // Define a posição do Minor como a posição atual deste gameObject
        }
    }
}
