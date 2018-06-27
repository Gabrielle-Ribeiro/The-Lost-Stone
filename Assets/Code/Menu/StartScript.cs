using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {

	public GameObject Panel;//painel que recebe o canvas da cena inteira 

	public void StartGame(){
		Panel.gameObject.SetActive (false);	//o canvas não sofre unload então seus elementos recebem a inativação com o set active
		SceneManager.LoadScene ("Level1", LoadSceneMode.Additive);//load da cena
		SceneManager.UnloadSceneAsync ("Menu");//enload da cena inicial do menu 
	}
}
