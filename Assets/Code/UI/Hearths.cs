using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearths : MonoBehaviour {
	public Image hearth;

	public void Damage (){
		hearth.fillAmount -= 0.10f; //decremento do fill amount para a renderização do coração 
	}
}
