using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorIA : MonoBehaviour
{
    // Velocidade do inimigo
    public float speed = 1.5f;

    // Direção inicial do inimigo
    Vector2 direction = Vector2.right;

    void Start () {
        // Define a direção inicial do npc
        transform.localScale = new Vector2 (-1 * transform.localScale.x, transform.localScale.y);
    }

    void FixedUpdate () {
        // Controla a velocidade do npc
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    // Se o personagem atingir o vilão ele morre
    // caso contrário o personagem morre
    void OnCollisionEnter2D(Collision2D col)
    {
        // Se acertar uma bala no personagem principal
        // TODO: freeze e animaçao de morte
        if (col.gameObject.tag == "Shot")
            DestroyObject(gameObject, 0);

        //Se nosso inimigo acertar o personagem
        if (col.gameObject.tag == "Player")
            DestroyObject(gameObject, 0);

        // Muda a direção caso colida com algo
        if (!col.gameObject.CompareTag("Ground"))
        {
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            direction = new Vector2(-1 * direction.x, direction.y);
        }
    }


}