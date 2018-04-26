using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharMove : MonoBehaviour {

	/* As variáveis a seguir serão utilizadas na alteração da direção que o MainChar está
	 * olhando.
	 * A variavel direcao quando possui valor true indica que o valor na propriedade Scale do
	 * componente Transform do objeto MainChar é positivo. O valor false indica o oposto.
	 */
	public bool direcao = true;
	public Transform mainCharTransform;

	// As variáveis a seguir serão usadas na movimentação do personagem
	public float speed = 2.5f, force = 1.5f;
	public Rigidbody2D mainCharRigidbody;
	public bool jump = false;

	//Limites de movimentação do MainChar
	public bool allowMovRight = true;
	public bool allowMovLeft = true;


	void Start () {
		/* Indica que com a variável mainCharTransform será possível manipular os valores das 
		 * propriedades do componente Transform do objeto MainChar
		 */
		mainCharTransform = GetComponent<Transform> ();

		mainCharRigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		
		if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !direcao){
			Flip ();
		}

		else if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && direcao){
			Flip ();
		}

		if((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && allowMovRight == true){
			transform.Translate (new Vector2 (speed * Time.deltaTime, 0));
		}

		if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && allowMovLeft == true){
				transform.Translate (new Vector2 (-speed * Time.deltaTime, 0));
			}

		if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && jump == true){
			mainCharRigidbody.AddForce(new Vector2 (0, force), ForceMode2D.Impulse);
		}


	}

	// O método Flip altera a direção que o personagem está olhando
	void Flip(){

		// O valor booleano da variável direcao é alterado 
		direcao = !direcao;

		/* O Vector3 scale pega os valores da propriedade Scale do componente Transform do 
		 * objeto MainChar.
		 * Depois é feita uma operação com a coordenada no eixo x da Scale do objeto MainChar, 
		 * que transforma o seu valor em seu oposto.
		 * Logo após, esse novo valor é passado para o componente Transform do objeto MainChar.
		 */
		Vector3 scale = mainCharTransform.localScale;
		scale.x *= -1;
		mainCharTransform.localScale = scale;
	}

	// pulo
	void OnCollisionEnter2D(Collision2D ground){
		if(ground.gameObject.CompareTag("Ground")){
			jump = true;
		}
	}

	// pulo
	void OnCollisionExit2D(Collision2D ground){
		if(ground.gameObject.CompareTag("Ground")){
			jump = false;
		}
	}

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

}
