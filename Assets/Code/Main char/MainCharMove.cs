using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCharMove : MonoBehaviour {

	//Variávies da Interface de usuário 
	public int pontuation;
    public Text points;
	public Image hearth;
 
	// Variáveis usadas na alteração da direção que o MainChar está olhando
	public bool direction = true;
	public Transform mainCharTransform;

	// Variáveis usadas na movimentação do personagem
	public float speed = 0.5f, force = 5.0f;
	public Rigidbody2D mainCharRigidbody;
	public bool jump = false;

	// Variáveis usadas na criação do tiro do MainChar
	public float shotingRate = 0.1f;    // Tempo entre um tiro e outro
	public float shotCoolDown = 0f;
	public Transform barrel;            // GameObject Empty que cria a bala
	public GameObject shot;             // GameObject da bala 

    // Variável que determina a animação do MainChar
    public Animator AnimController;

    // Variáveis para o controle de vida do personagem
	public int life = 10;
	public bool playerIsAlive = true;

    // Variáveis utilizadas nos audios
    public float vol;
    public AudioClip audFire;
    AudioSource AudController;

    Scene faseAtual;

    void Start () {

        faseAtual = SceneManager.GetActiveScene();

        PlayerPrefs.GetInt("Pontuacao", pontuation);
        PlayerPrefs.GetInt("Life", life);

        /* Indica que com a variável mainCharTransform será possível manipular os valores das 
		 * propriedades do componente Transform do objeto MainChar
		 */
        mainCharTransform = GetComponent<Transform> ();

		/* Indica que com a variável mainCharRigidbody será possível manipular os valores das 
		 * propriedades do componente Rigidbody 2D do objeto MainChar
		 */
		mainCharRigidbody = GetComponent<Rigidbody2D> ();

        // Atribui o componente AudioSource a AudController
        AudController = GetComponent<AudioSource>();
        AnimController = GetComponent<Animator>();

        AnimController.SetBool("inGround", false);
        AnimController.SetBool("isWalking", false);
        AnimController.SetBool("isJumping", true);
        AnimController.SetBool("isFiring", false);
    }

	void Update () {
		
		if(playerIsAlive){
			// Mudança da direção que o MainChar está olhando
			if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !direction){
				Flip ();
			}
			else if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && direction){
				Flip ();
			}

       		 /* Movimentação do MainChar para a direita, esquerda e pulo de acordo com a tecla que o
			 * usuário está apertando. 
			 */
        	if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))){
				//pontuationText.text = "RIGHT";
          	  AnimController.SetBool("isWalking", true);
           	 transform.Translate(new Vector2(speed * Time.deltaTime, 0));
       		 }

        	else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))){
				//pontuationText.text = "LEFT";
           	 	AnimController.SetBool("isWalking", true);
            	transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
        	}

        	else
            	AnimController.SetBool("isWalking", false);

        	if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jump == true){
            	mainCharRigidbody.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            	AnimController.SetBool("isJumping", true);
            	AnimController.SetBool("inGround", false);
       	 	}

        	// Controle do tempo entre um tiro e outro
        	if (shotCoolDown > 0)
				shotCoolDown -= Time.deltaTime;

			// Libera um tiro (GameObject Shot) quando o usuário clica na tecla Space
			if(Input.GetKey(KeyCode.Space)){
            	AnimController.SetBool("isFiring", true);
				Fire ();
				shotCoolDown = shotingRate;

            	// Toca o audio audFire
            	AudController.PlayOneShot(audFire, vol);
			}
       		else
            	AnimController.SetBool("isFiring", false);
		}

        PlayerPrefs.SetInt("Pontuacao", pontuation);
			
	}

	// O método Flip altera a direção que o personagem está olhando
	void Flip(){
		direction = !direction;
		Vector3 scale = mainCharTransform.localScale;
		scale.x *= -1;
		mainCharTransform.localScale = scale;
	}

	// Método que faz alguma ação ao colidir com algo
	void OnCollisionEnter2D(Collision2D col){
        // Método que faz com que o MainChar possa pular caso esteja em contado com o chão
        if (col.gameObject.CompareTag("Ground")){
            AnimController.SetBool("inGround", true);
            AnimController.SetBool("isJumping", false);
			jump = true;
		}

		if (col.gameObject.CompareTag("Enemy")){
			hearth.fillAmount -= 0.10f; //decremento do fill amount para a renderização do coração 
			life -= 1;
			if(life == 0){
				MainCharDead ();
			}
		}
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // HOTFIX: Pular ao passar por baixo de uma plataforma
        if (collision.gameObject.CompareTag("Ground"))
        {
            jump = true;

            AnimController.SetBool("isJumping", false);
            AnimController.SetBool("inGround", true);
        }
    }

    void OnCollisionExit2D(Collision2D coll){

        // Método que impede o MainChar de pular caso não esteja em contado com o chão
        if (coll.gameObject.CompareTag("Ground")){
            AnimController.SetBool("inGround", false);
            AnimController.SetBool("isJumping", true);
			jump = false;
		}
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Se o MainChar cair em um "buraco" ele perde todas as suas vidas
        if (coll.gameObject.CompareTag("Hole"))
        {
            life = 0;
			hearth.fillAmount = 0;
			MainCharDead ();
            //SceneManager.LoadLevel("GameOver");
        }
		// Se o player colidir com um item ele ganha 10 pontos
        if (coll.gameObject.CompareTag("PontuationItem"))
        {
			pontuation = pontuation + 10;
			points.text = pontuation.ToString();
        }
		// Se o player colidir com um vida ele ganha uma vida
		if (coll.gameObject.CompareTag("Life") && life < 10)
		{
			hearth.fillAmount += 0.20f;
            PlayerPrefs.SetInt("Life", life);
		}
    }
		
	// Método de criação do GameObject Shot
	void Fire(){
		if(shotCoolDown <= 0f){
			if(shot != null){ //Verifica se o GameObject shot foi criado
				var cloneShot = Instantiate(shot, barrel.position, Quaternion.identity) as GameObject;
				cloneShot.transform.localScale = this.transform.localScale;
			}
		}
	}

	// Método que controla a morte do MainChar
	void MainCharDead(){
		playerIsAlive = false;
		SceneManager.LoadScene ("GameOver", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(faseAtual);
    }
		

}