using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProximaFase : MonoBehaviour
{
    Scene faseAtual;
    int proximaFase;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.gameObject.CompareTag("Player"))
        {
            proximaFase = SceneManager.GetActiveScene().buildIndex + 1;
            PlayerPrefs.SetInt("FaseSalva", proximaFase);

            SceneManager.LoadScene(proximaFase, LoadSceneMode.Single);//load da cena
            SceneManager.UnloadSceneAsync(proximaFase - 1);//enload da cena inicial do menu 
        }

    }
}
