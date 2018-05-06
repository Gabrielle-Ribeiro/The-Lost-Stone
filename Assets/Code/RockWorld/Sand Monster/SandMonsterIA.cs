using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandMonsterIA : MonoBehaviour {

    public int life;
    public int playerDamage;

    public bool lostPlayer;
    public bool isDead;
    public bool isWaiting;

    public float AttackCooldown;    // Tempo restante para o próximo ataque
    float AttackCooldownDefault;    // Tempo entre dois ataques

    Animator AnimController;

    public Transform sandMonsterThrown;    // prefab do Attack

    Transform sandMonster;
    Transform target;               // Armazena o local do alvo (Jogador)

    Rigidbody2D rgbd_sandMonster;
    
	void Start () {
        sandMonster = GetComponent<Transform>();
        AnimController = GetComponent<Animator>();
        rgbd_sandMonster = GetComponent<Rigidbody2D>();
        
        // Define o tempo que o npc deve aguardar até poder atacar novamente
        AttackCooldownDefault = AttackCooldown;

        // Reduz o tempo restante, o npc começa pronto para atacar, afinal, ele ainda nem atacou
        AttackCooldown = 0.5f;

        // Define o alvo
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        lostPlayer = true;
        isDead = false;
        isWaiting = true;
    }
	
	void Update () {
        if (AttackCooldown > 0 && !isWaiting)
            AttackCooldown -= Time.deltaTime;

        if (!isDead && !lostPlayer)
            AttackRanged();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lostPlayer = false;
            isWaiting = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lostPlayer = true;
        }
    }


    void AttackRanged()
    {
        if (AttackCooldown > 0)
            AttackCooldown -= Time.deltaTime;

        else
        {
            AttackCooldown = AttackCooldownDefault;

            var shotTransform = Instantiate(sandMonsterThrown) as Transform;
            shotTransform.position = transform.position;
        }
    }
}
