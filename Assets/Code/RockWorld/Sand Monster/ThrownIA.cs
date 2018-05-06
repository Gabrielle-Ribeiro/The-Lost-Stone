using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownIA : MonoBehaviour {
    public float speed;

    public Transform sandMonsterMinor;  // Prefab do npc a ser criado quando houver uma colisão

    Rigidbody2D rgbd_Thrown;

    void Start()
    {
        rgbd_Thrown = GetComponent<Rigidbody2D>();

        rgbd_Thrown.velocity = new Vector2(-speed, 0) * transform.localScale.x;
    }
	
	void Update () {

	}

    void OnCollisionEnter2D(Collision2D col)
    {
        // Se acertar uma bala no personagem principal
        if (col.gameObject.CompareTag("Shot"))
        {
            DestroyObject(gameObject, 0);
        }

        // Se acertar o personagem principal
        else if (col.gameObject.CompareTag("Player"))
            DestroyObject(gameObject, 0);

        // Se acertar qualquer outra coisa menos se for algum sandMonster grande, cria o monstro menor
        else if (!col.gameObject.CompareTag("SandMonster"))
        {
            DestroyObject(gameObject, 0);

            var BulletTransform = Instantiate(sandMonsterMinor) as Transform;
            BulletTransform.position = transform.position;
        }
    }
}
