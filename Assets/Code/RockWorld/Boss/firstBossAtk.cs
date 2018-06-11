using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstBossAtk : MonoBehaviour {

    public float Cooldown;      // Tempo restante para o proximo ataque a distancia
    public float range;         // Alcance do ataque a distância

    float _cooldown;            // Tempo entre dois ataques a distância

    public Transform Target;

    public Transform AcidRanged;
    public Transform AcidPool;

    public Transform AcidSpawn;
    public Transform PoolSpawn;

    BossMov boss;               // Permite modificar variaveis no script BossMov
    Animator AnimController;

    void Start () {
        AnimController = GetComponent<Animator>();

        // Permite modificar variaveis no script BossMov
        boss = (BossMov)gameObject.GetComponent(typeof(BossMov));

        _cooldown = Cooldown;
    }
	
	// Update is called once per frame
	void Update () {
        Cooldown -= Time.deltaTime;

        // Caso o jogador esteja no alcance e o boss esteja pronto, o boss ataca
        if (inRange() && Cooldown <= 0)
            AnimController.SetBool("Ranged", true);
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Quando o jogador se aproxima do Boss, a movimentação do Boss é parada e o ataque é ativado
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.canWalk = false;
            AnimController.SetBool("Melee", true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Quando o jogador se afasta do Boss, o Boss volta a se movimentar
        if (collision.gameObject.CompareTag("Player"))
            boss.canWalk = true;
    }

    // Controla os ataques corpo a corpo
    // Parametro recebido pelo Animator
    void MeleeAttack(string Action)
    {
        // Cria uma "piscina" de acido
        if (Action == "Attack")
        {
            var Bullet = Instantiate(AcidPool) as Transform;
            Bullet.position = PoolSpawn.position;
        }
        
        // Termina o ataque
        else if (Action == "Reset")
            AnimController.SetBool("Melee", false);
    }

    bool inRange()
    {
        // Calcula a distância entre o jogador e boss
        // Trigger evitado devido o boss já ter 18555 triggers
        float distance = Vector3.Distance(transform.position, Target.transform.position);

        if (distance <= range)
            return true;

        else
            return false;
    }

    // Controla os ataques a distancia
    // Parametro recebido pelo Animator
    void RangedAttack (string Action)
    {
        // Cospe acido no jogador
        if (Action == "Attack")
        {
            var Bullet = Instantiate(AcidRanged) as Transform;
            Bullet.position = AcidSpawn.position;
        }

        // Termina o ataque e reseta o Cooldown
        else if (Action == "Reset")
        {
            Cooldown = _cooldown;

            AnimController.SetBool("Ranged", false);
        }
    }
}
