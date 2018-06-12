using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecBoss : MonoBehaviour {

    public Transform woundPrefab;
    public Transform BossHome;
    public bool canAttack;
    public float speed;
    public bool isDead;
    public bool lostPlayer;

    public float AttackRate;
    public float AttackCooldown;

    public int life;
    public int playerDamage;
    public int AmoDam;

    Transform secBoss;
    Transform target;
    Transform beak;
    Rigidbody2D rgbd_secBoss;

    void Start()
    {
        canAttack = false;
        secBoss = GetComponent<Transform>();
        BossHome = secBoss.transform.parent;
        beak = GameObject.FindGameObjectWithTag("Beak").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rgbd_secBoss = GetComponent<Rigidbody2D>();
        isDead = false;
        lostPlayer = true;
        AmoDam = 0;

    }

    void Update()
    {
        if (AttackCooldown > 0 && !lostPlayer && !isDead)
        {
            AttackCooldown -= Time.deltaTime;
        }
        else
        {
                Attack();
        }

        if (canAttack)
        {

            if (!isDead)
            {
                secBoss.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
           
            }

        }
        if (!canAttack){
            BacktoHome();
        }
        if (AmoDam > 3)
        {
            isDead = true;
        }
    }
    public void BacktoHome()
    {
        secBoss.position = Vector2.Lerp(transform.position, BossHome.position, 0.01f);
    }


    public void Attack()
    {

        if (AttackCooldown <= 0)
        {
            AttackCooldown = AttackRate;
            canAttack = true;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // dano personagem
            canAttack = false;
            BacktoHome();
        }
        if (col.gameObject.CompareTag("Ground"))
        {
            var wound = Instantiate(woundPrefab) as Transform;
            float x = beak.position.x;
            wound.position = new Vector3(x, 2.17f, 0);
            canAttack = false;
            BacktoHome();
            AmoDam++;
        }
    }
    
}