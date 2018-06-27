using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdBoss : MonoBehaviour
{


    public float speed;              
    public bool canAttack;            
    public bool isDead;             
    public bool isWaiting;          
    public bool LookingAtRight;    

    public int life;               
    public int playerDamage;
    public AudioClip soundBoss; //Variáveis do som
    public AudioClip attackBoss;

    Vector2 direction = Vector2.left;

    Transform thirdBoss;          
    Transform target;               

    Rigidbody2D rgbd_thirdBoss;  

    void Start()
    {
       
        thirdBoss = GetComponent<Transform>();            
        rgbd_thirdBoss = GetComponent<Rigidbody2D>();     

        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        isWaiting = true;
        canAttack = false;
        isDead = false;
        LookingAtRight = false;
        SoundManager.instance.PlaySingle(soundBoss);
    }

    void Update()
    {
        if (canAttack && !isWaiting)
        {
            
            if (!isDead)
            {
                
                if (target.position.x > transform.position.x && LookingAtRight == false)
                {
                    Vector2 scale = thirdBoss.localScale;     
                    scale.x *= -1;                              
                    thirdBoss.localScale = scale;             

                    LookingAtRight = true;
                }

               
                else if (target.position.x < transform.position.x && LookingAtRight == true)
                {
                    Vector3 scale = thirdBoss.localScale;
                    scale.x *= -1;
                    thirdBoss.localScale = scale;

                    LookingAtRight = false;
                }

                thirdBoss.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);    // Segue o Jogador
               
            }

        }
    }
    void FixedUpdate()
    {
        if (!canAttack)
        GetComponent<Rigidbody2D>().velocity = direction * speed;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isWaiting && !collision.gameObject.CompareTag("Ground"))
            isWaiting = false;

        
        if (collision.gameObject.tag == "Shot")
        {
            if (target.position.x < transform.position.x && direction == Vector2.right)
            {
                transform.localScale = new Vector2(-1 * transform.localScale.x,
                transform.localScale.y);

                direction = new Vector2(-1 * direction.x, direction.y);
            }
            if (target.position.x > transform.position.x && direction == Vector2.right)
            {
                transform.localScale = new Vector2(-1 * transform.localScale.x,
                transform.localScale.y);

                direction = new Vector2(-1 * direction.x, direction.y);
            }
            
            canAttack = true;
            life -= playerDamage;
            if (life < 1)
            {
                GetComponent<Collider2D>().enabled = false;                             
                rgbd_thirdBoss.constraints = RigidbodyConstraints2D.FreezePosition;   
                //animação morrendo
                isDead = true;
                canAttack = false;
                
            }
            
        }
        
        if (canAttack)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SoundManager.instance.PlaySingle(attackBoss);
                //animação de dano
            }

        }

        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!canAttack)
        {
        
            transform.localScale = new Vector2(-1 * transform.localScale.x,
                transform.localScale.y);

            direction = new Vector2(-1 * direction.x, direction.y);
        }
    }

}
