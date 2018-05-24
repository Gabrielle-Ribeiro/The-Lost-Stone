using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientTree : MonoBehaviour {

    /*Ancient Tree:
     *  -Permanece imóvel até o jogador acorda-lo (atirar ou colidir);
     *  -Persegue o jogador até ele sair de perto;
    */

    // Todas as variáveis com public são definidas na Unity / Inspector

    public float speed;             // Define a velocidade do npc

    public bool lostPlayer;         // Caso o jogador esteja dentro do campo de visão do npc, lostPlayer = false;
    public bool canWalk;            // Permite o npc se mover caso !isWaiting && !isDead
    public bool isDead;             // Caso o npc esteja morto, isDead = true;
    public bool isWaiting;          // Está "dormindo", se o jogador ainda não interagiu com o npc: isWaiting = true;
    public bool LookingAtRight;     // Direção do npc

    public int life;                // Vida do npc
    public int playerDamage;        // Dano que o jogador causa a este npc

    Animator AnimController;        // Controla as animações do npc

    Transform ancientTree;          // Permite mudar a posição do npc
    Transform target;               // Armazena o local do alvo (Jogador)

    Rigidbody2D rgbd_ancientTree;   // Permite freezar a posição do npc ao morrer
    
	void Start () {
        // Definindo os componentes do npc
        // Permite:
        ancientTree = GetComponent<Transform>();            // Ler e modificar dados referentes a posição
        AnimController = GetComponent<Animator>();          // Modificar variaveis para definir animação
        rgbd_ancientTree = GetComponent<Rigidbody2D>();     // Congelar posição atual

        // Definindo o jogador como alvo
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // Define animação atual como Standing
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
                // Muda a direção do npc se o jogador estiver a direita do npc && !LookingAtRight 
                if (target.position.x > transform.position.x && LookingAtRight == false)
                {
                    Vector2 scale = ancientTree.localScale;     // Copia a escala atual
                    scale.x *= -1;                              // Inverte o eixo X, flip();
                    ancientTree.localScale = scale;             // Aplica ao npc

                    LookingAtRight = true;
                }

                // Muda a direção do npc se o jogador estiver a esquerda do npc && LookingAtRight
                else if (target.position.x < transform.position.x && LookingAtRight == true)
                {
                    Vector3 scale = ancientTree.localScale;     
                    scale.x *= -1;                              
                    ancientTree.localScale = scale;             

                    LookingAtRight = false;
                }

                ancientTree.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);    // Segue o Jogador
                AnimController.SetBool("isWalking", true);
            }

            else
                AnimController.SetBool("isWalking", false);
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se a bala acertar o npc
        if (collision.gameObject.tag == "Shot")
        {
            life -= playerDamage;

            // Caso o npc morra ao receber dano
            if (life < 1)
            {
                GetComponent<Collider2D>().enabled = false;                             // Desliga os colliders
                rgbd_ancientTree.constraints = RigidbodyConstraints2D.FreezePosition;   // Congela posição atual

                isDead = true;

                // Correção bug: Arvore Fantasma
                canWalk = false;
                
                AnimController.SetBool("isDying", true);
            }

            // TODO: animação entra em conflito caso isTakingDamage == true;
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
		// Acorda o npc caso ele esteja dormindo
		if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Shot"))
		{
			lostPlayer = false;
			if (!canWalk)
				canWalk = true;

			if (isWaiting)
				isWaiting = false;
		}       
    }

    void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("Player")){
            lostPlayer = true;
		}
    }
}
