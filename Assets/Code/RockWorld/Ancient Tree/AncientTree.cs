using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientTree : MonoBehaviour {

    /*Ancient Tree:
     *  -Permanece imóvel até o jogador acorda-lo (atirar ou colidir);
     *  -Persegue o jogador até ele sair de perto;
     *  
     *  TODO:
     *  flip() caso o personagem mude de direção
    */

    public float speed;

    public bool lostPlayer;
    public bool canWalk;
    public bool isDead;
    public bool isWaiting;
    public bool LookingAtRight;

    public int life;                // Vida do personagem
    public int playerDamage;        // Dano que o jogador causa a este npc

    Animator AnimController;

    Transform ancientTree;
    Transform target;               // Armazena o local do alvo (Jogador)

    Rigidbody2D rgbd_ancientTree;   // Permite freezar a posição do npc ao morrer

	void Start () {
        ancientTree = GetComponent<Transform>();
        AnimController = GetComponent<Animator>();
        rgbd_ancientTree = GetComponent<Rigidbody2D>();

        // Definindo o alvo
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // Definindo animação Standing
        AnimController.SetBool("isWalking", false);
        AnimController.SetBool("isAttacking", false);
        AnimController.SetBool("isTakingDamage", false);
        AnimController.SetBool("isDying", false);

        isWaiting = true;
        lostPlayer = true;
        canWalk = false;
        isDead = false;
        LookingAtRight = false;
    }
	
	void Update () {
        if (canWalk && !isWaiting)
        {
            // Caso o jogador esteja no "campo de visão" do npc, ele o persegue
            if (!lostPlayer && !isDead)
            {
                if (target.position.x > transform.position.x && LookingAtRight == false)
                {
                    Vector3 scale = ancientTree.localScale;
                    scale.x *= -1;
                    ancientTree.localScale = scale;

                    LookingAtRight = true;
                }

                else if (target.position.x < transform.position.x && LookingAtRight == true)
                {
                    Vector3 scale = ancientTree.localScale;
                    scale.x *= -1;
                    ancientTree.localScale = scale;

                    LookingAtRight = false;
                }

                ancientTree.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                AnimController.SetBool("isWalking", true);
            }

            else
                AnimController.SetBool("isWalking", false);
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Acorda o npc caso ele esteja dormindo
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Shot"))
        {
            if (!canWalk)
                canWalk = true;

            if (isWaiting)
                isWaiting = false;
        }

        // Se a bala acertar o npc
        if (collision.gameObject.tag == "Shot")
        {
            life -= playerDamage;

            // Caso o npc morra ao receber dano
            if (life < 1)
            {
                GetComponent<Collider2D>().enabled = false;
                rgbd_ancientTree.constraints = RigidbodyConstraints2D.FreezePosition;

                isDead = true;

                // Correção bug: Arvore Fantasma
                canWalk = false;
                
                AnimController.SetBool("isDying", true);
            }

            // Caso o npc receba dano mas continue vivo
            //else if (life > 0)
                //AnimController.SetBool("isTakingDamage", true);
        }

        // Se o jogador colidir com o npc
        if (collision.gameObject.CompareTag("Player"))
        {
            canWalk = true;
            AnimController.SetBool("isAttacking", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            AnimController.SetBool("isAttacking", false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            lostPlayer = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Player")){
            lostPlayer = true;
		}
    }
}
