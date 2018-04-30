using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharMove : MonoBehaviour {

	// Variáveis usadas na alteração da direção que o MainChar está olhando
	public bool direction = true;
	public Transform mainCharTransform;

	// Variáveis usadas na movimentação do personagem
	public float speed = 0.5f, force = 5.0f;
	public Rigidbody2D mainCharRigidbody;
	public bool jump = false;

	// Variáveis que determinam os limites de movimentação do MainChar
	public bool allowMovRight = true;
	public bool allowMovLeft = true;

	// Variáveis usadas na criação do tiro do MainChar
	public float shotingRate = 0.1f; // Tempo entre um tiro e outro
	public float shotCoolDown = 0f;
	public Transform barrel; // GameObject Empty que cria a bala
	public GameObject shot; // GameObject da bala 



	void Start () {
		/* Indica que com a variável mainCharTransform será possível manipular os valores das 
		 * propriedades do componente Transform do objeto MainChar
		 */
		mainCharTransform = GetComponent<Transform> ();

		/* Indica que com a variável mainCharRigidbody será possível manipular os valores das 
		 * propriedades do componente Rigidbody 2D do objeto MainChar
		 */
		mainCharRigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update () {

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
		if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && allowMovRight == true){
			transform.Translate (new Vector2 (speed * Time.deltaTime, 0));
		}
		if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && allowMovLeft == true){
			transform.Translate (new Vector2 (-speed * Time.deltaTime, 0));
		}
		if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && jump == true){
			mainCharRigidbody.AddForce(new Vector2 (0, force), ForceMode2D.Impulse);
		}

		// Controle do tempo entre um tiro e outro
		if(shotCoolDown > 0){
			shotCoolDown -= Time.deltaTime;
		}
		// Libera um tiro (GameObject Shot) quando o usuário clica nas teclas M ou J
		if(Input.GetKey(KeyCode.M) || Input.GetKey(KeyCode.J)){
			Fire ();
			shotCoolDown = shotingRate;
		}
	}

	// O método Flip altera a direção que o personagem está olhando
	void Flip(){
		direction = !direction;
		Vector3 scale = mainCharTransform.localScale;
		scale.x *= -1;
		mainCharTransform.localScale = scale;
	}

	// Método que faz com que o MainChar possa pular caso esteja em contado com o chão
	void OnCollisionEnter2D(Collision2D ground){
		if(ground.gameObject.CompareTag("Ground")){
			jump = true;
		}
	}

	// Método que impede o MainChar de pular caso não esteja em contado com o chão
	void OnCollisionExit2D(Collision2D ground){
		if(ground.gameObject.CompareTag("Ground")){
			jump = false;
		}
	}

	/* Os próximos dois métodos limitam a movimentação do MainChar numa área pré 
	 * definida da tela
	 */
	void OnTriggerEnter2D(Collider2D limit){
		if(limit.gameObject.CompareTag("LimitRight")){
			allowMovRight = false;
		}
		else if(limit.gameObject.CompareTag("LimitLeft")){
			allowMovLeft = false;
		}
	}

	void OnTriggerExit2D(Collider2D limit){
		if(limit.gameObject.CompareTag("LimitRight")){
			allowMovRight = true;
		}
		else if(limit.gameObject.CompareTag("LimitLeft")){
			allowMovLeft = true;
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
}