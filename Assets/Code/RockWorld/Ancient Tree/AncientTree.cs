using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientTree : MonoBehaviour {

    /*Ancient Tree:
     *  -Permanece imóvel até o jogador collidir com ela;
     *  -Persegue o jogador até ele sair de perto;
     *  
     *  TODO:
     *  flip() caso o personagem mude de direção
    */

    public float speed;
    public float home;

    public bool lostPlayer;
    public bool canWalk;
    public bool isDead;
    public bool isWaiting;
    public bool LookingRight;

    public int life;
    public int playerDamage;

    Animator AnimController;

    Transform player;
    Transform ancientTree;

    Rigidbody2D rgbd_ancientTree;

	void Start () {
        AnimController = GetComponent<Animator>();
        ancientTree = GetComponent<Transform>();
        rgbd_ancientTree = GetComponent<Rigidbody2D>();

        home = transform.position.x;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        AnimController.SetBool("isWalking", false);
        AnimController.SetBool("isAttacking", false);
        AnimController.SetBool("isTakingDamage", false);
        AnimController.SetBool("isDying", false);

        isWaiting = true;
        lostPlayer = true;
        canWalk = false;
        isDead = false;
        LookingRight = false;
    }
	
	void Update () {
        if (canWalk)
        {
            //Caso o Jogador esteja no "campo de visão" da arvore, ela o persegue
            if (!lostPlayer && !isDead)
            {
                ancientTree.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                AnimController.SetBool("isWalking", true);
            }

            //else if (lostPLayer)
            //Voltar a origem

            else
                AnimController.SetBool("isWalking", false);
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Acorda a arvore caso ela esteja dormindo
        if (isWaiting)
            isWaiting = false;

        //Se a bala acertar nosso inimigo
        if (collision.gameObject.tag == "Shot")
        {
            life -= playerDamage;

            //Caso a arvore morra ao receber dano
            if (life < 1)
            {
                //Congelando o sprite e desabilitando o colisor
                GetComponent<Collider2D>().enabled = false;
                rgbd_ancientTree.constraints = RigidbodyConstraints2D.FreezePosition;

                isDead = true;
                canWalk = false;
                
                AnimController.SetBool("isDying", true);
            }

            //Caso a arvore tome dano
            else if (life > 0)
                AnimController.SetBool("isTakingDamage", true);
        }

        //Se o jogador colidir com a arvore
        if (collision.gameObject.CompareTag("Player"))
        {
            canWalk = true;
            AnimController.SetBool("isAttacking", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AnimController.SetBool("isAttacking", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lostPlayer = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lostPlayer = true;
            canWalk = true;
        }
    }
}
