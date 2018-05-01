using System.Collections;
using UnityEngine;

public class TreeIA : MonoBehaviour {

    public Transform treeHome;
    private Transform player;
    private Vector3 positionPlayerLost;
    private Vector3 positionPlayerFind;
    private Transform tree;

    public float speed;
    private float startTime;
    private float journeyLeght;

    public bool lostPlayer = true;
    public bool canWalk = false;
    public bool dead = false;

    public int life;

    public Animator AnimController;

	// Use this for initialization
	void Start () {
        life = 3;

        tree = GetComponent<Transform>();
        treeHome = tree.transform.parent;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        positionPlayerLost = treeHome.position;
        BacktoHome ();

        AnimController.SetBool("isWalking", false);
        AnimController.SetBool("isAttacking", false);
        AnimController.SetBool("isTakingDamage", false);
        AnimController.SetBool("isDying", false);
	}
   
    // Update is called once per frame
    void Update ()
    {
        if (canWalk)
            AnimController.SetBool("isWalking", true);

        else
            AnimController.SetBool("isWalking", false);

        if (canWalk)
            if (lostPlayer)
            {
                float dist = (Time.time - startTime) * speed;
                float journey = dist / journeyLeght;


                if (tree.position == treeHome.position)
                    canWalk = false;

                tree.position = Vector3.Lerp(positionPlayerLost, treeHome.position, journey);
            }
            else
            {
                if (dead == false)
                    tree.position = Vector3.Lerp(tree.position, player.position, 0.01f);
            }
        
	}

    public void BacktoHome()
    {
        if (!dead)
        {
            startTime = Time.time;
            positionPlayerLost = tree.position;
            journeyLeght = Vector3.Distance(positionPlayerLost, treeHome.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        lostPlayer = false;
        Debug.Log("Colisão com Player");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Se a bala acertar nosso inimigo
        if (col.gameObject.name == "ShotMainChar(Clone)")
        {
            if (life == 1)
            {
                AnimController.SetBool("IsAttacking", false);
                AnimController.SetBool("IsTakingDamage", false);
                AnimController.SetBool("isDying", true);
                dead = true;
                
                GetComponent<Collider2D>().enabled = false;
                DestroyObject(gameObject, 3);
            }

            //TODO: uma vez dentro da animaçao TakingDamage, ele não volta
            else if (life > 1)
                AnimController.SetBool("isTakingDamage", true);

            DestroyObject(col.gameObject, 0);
            life--;
        }

        //Se nosso inimigo acertar o personagem
        if (col.gameObject.name == "MainChar")
        {
            AnimController.SetBool("isAttacking", true);
            //Personagem principal toma dano
        }

        else
            AnimController.SetBool("isAttacking", false);
    }
    
}
