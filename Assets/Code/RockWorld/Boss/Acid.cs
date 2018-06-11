using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour {

    public bool inAir;      // Váriavel de controle / Animator
    public float speed;     // Velocidade do arremessável
    
    public Transform Pool;  // Prefab da "piscina" de ácido

    Rigidbody2D rgbd;
    Animator AnimController;

    void Start () {
        rgbd = GetComponent<Rigidbody2D>();
        AnimController = GetComponent<Animator>();

        inAir = true;
        AnimController.SetBool("inAir", inAir);

        Attack();
    }
	
	void Update () {

        // Caso não esteja no ar, inicia a animação Splash
        if (!inAir)
            AnimController.SetBool("inAir", inAir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Corrige a rotação do gameObject
        if (!collision.gameObject.CompareTag("Boss"))
            rgbd.rotation = 0;

        if (collision.gameObject.CompareTag("Ground"))
            inAir = false;

        // Caso acerte o jogador, o arremessável é destruido
        if (collision.gameObject.CompareTag("Player"))
            DestroyObject(gameObject);
    }

    void Attack()
    {
        rgbd.velocity = new Vector2(-speed, 0) * transform.localScale.x;
    }

    // Transforma o arremessável em uma "piscina" de acido
    // Método chamado na animação
    void TransformIntoPool()
    {
        DestroyObject(gameObject);

        var BulletTransform = Instantiate(Pool) as Transform;   
        BulletTransform.position = transform.position;                      
    }
}
