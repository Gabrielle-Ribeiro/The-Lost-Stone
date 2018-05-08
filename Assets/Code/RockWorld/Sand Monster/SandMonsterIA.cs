using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandMonsterIA : MonoBehaviour {

    public int life = 15;

    public bool lostPlayer;
    public bool isDead;
    public bool isWaiting;

    public float AttackCooldown;    // Tempo restante para o próximo ataque
    float AttackCooldownDefault;    // Tempo entre dois ataques

    Animator AnimController;

    public Transform sandMonsterThrown; // prefab do Attack (Thrown)
    public Transform sandMonsterMinor;  // Prefab do SandMonsterMinor

    Rigidbody2D rgbd_sandMonster;
    
	void Start () {
        AnimController = GetComponent<Animator>();
        rgbd_sandMonster = GetComponent<Rigidbody2D>();
        
        // Define o tempo que o npc deve aguardar até poder atacar novamente
        AttackCooldownDefault = AttackCooldown;

        // Reduz o tempo restante, o npc começa pronto para atacar, afinal, ele ainda nem atacou
        AttackCooldown = 1f;
        
        lostPlayer = true;
        isDead = false;
        isWaiting = true;

        AnimController.SetBool("isWaiting", true);
    }
	
	void Update () {
        if (AttackCooldown > 0 && !isWaiting)
            AttackCooldown -= Time.deltaTime;

        if (!isDead && !lostPlayer)
            AttackRanged();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shot"))
        {
            life -= 1;

            if (life == 1)
                TransformIntoMinor();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lostPlayer = false;
            isWaiting = false;

            AnimController.SetBool("isWaiting", false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            lostPlayer = true;
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

    void TransformIntoMinor()
    {
        DestroyObject(gameObject, 0);

        var BulletTransform = Instantiate(sandMonsterMinor) as Transform;
        BulletTransform.position = transform.position;
    }
}
