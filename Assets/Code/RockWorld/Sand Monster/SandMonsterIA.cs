using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandMonsterIA : MonoBehaviour {

    // Todas as variáveis com public são definidas na Unity / Inspector

    public int life;                    // Vida do npc
    public int playerDamage;            // Dano que o jogador causa a este npc

    public bool lostPlayer;             // Caso o jogador esteja dentro do campo de visão do npc, lostPlayer = false;
    public bool isDead;                 // Caso o npc esteja morto, isDead = true;
    public bool isWaiting;              // Caso o jogador ainda não interagiu com o npc, isWaiting = true;
    public bool isUpperGround;          // Caso npc não esteja escondido, isUpperGround = true;

    public float AttackCooldown;        // Tempo restante para o próximo ataque
    float AttackCooldownDefault;        // Tempo entre dois ataques

    Animator AnimController;            // Controla as animações do npc
    PolygonCollider2D cldr;

    public Transform sandMonsterThrown; // Prefab do Attack (Thrown)
    public Transform sandMonsterMinor;  // Prefab do SandMonsterMinor
    public Transform bulletSpawn;       // Local a ser criado o ataque
    //Variáveis do audio
    public AudioClip soundMonster1;
    public AudioClip soundattack;
	void Start () {
        // Definindo os componentes do npc
        // Permite:
        AnimController = GetComponent<Animator>();  // Modificar variaveis para definir animação
        cldr = GetComponent<PolygonCollider2D>();   // Modificar condições do colisor

        // Define o tempo que o npc deve aguardar até poder atacar novamente
        AttackCooldownDefault = AttackCooldown;

        // Reduz o tempo restante, o npc começa pronto para atacar, afinal, ele ainda nem atacou
        AttackCooldown = 1f;
        
        lostPlayer = true;
        isDead = false;
        isWaiting = true;

        AnimController.SetBool("isUpperGround", false);
    }
	
	void Update () {
        // Reduz o cooldown caso esteja acordado
        if (AttackCooldown > 0 && !isWaiting)    
        AttackCooldown -= Time.deltaTime;
        

        // Ataca o jogador 

        if (!isDead && !lostPlayer)
        AttackRanged();
     

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shot"))
        {
            life -= playerDamage;
            SoundManager.instance.PlaySingle(soundattack);

            // Se transforma em um Sand Monster Minor caso tenha pouca vida
            if (life < 2)
                TransformIntoMinor();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.gameObject.CompareTag("Player"))
            Appears (true);
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Appears (false);
       
    }


    void AttackRanged()
    {
     
        // Reduz o tempo restante para o proximo ataque se o mesmo for > 0
        if (AttackCooldown > 0)
            AttackCooldown -= Time.deltaTime;
       
        // Atira :D
        else if (isUpperGround)
        { 
            AttackCooldown = AttackCooldownDefault;     // Reinicia o cooldown 
            var shotTransform = Instantiate(sandMonsterThrown) as Transform;    // Instancia um golpe
            shotTransform.position = bulletSpawn.position;       // Define a posição do golpe como a posição atual deste npc


        }
        
    }

    // Função que torna o npc em um Minor
    void TransformIntoMinor()
    {
        DestroyObject(gameObject, 0);

        var BulletTransform = Instantiate(sandMonsterMinor) as Transform;   // Instancia um Minor
        BulletTransform.position = transform.position;                      // Define a posição do Minor como a posição atual deste npc
    }

    void Appears(bool show)
    {
        if (show)
        {
            SoundManager.instance.PlaySingle(soundMonster1);
            lostPlayer = false;
            isWaiting = false;
          
        }

        else
            lostPlayer = true;

        // Define o estado do npc (vísivel / não visível)
        AnimController.SetBool("isUpperGround", show);

        // Ativa / desativa o colisor do npc
        cldr.enabled = show;
    }

    void Combat (string Action)
    {
        if (Action == "Hide")
            isUpperGround = false;

        if (Action == "Show")
            isUpperGround = true;
    }
}
