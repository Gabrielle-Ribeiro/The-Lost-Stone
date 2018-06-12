using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioExample : MonoBehaviour {

    /*
        Cola:

        public AudioClip _nomeDaVariavel

        public -> permite que a variavel seja modificada por qualquer parte do código/programa,
            tambem torna a variavel visivel/manipulavel no Inspector dentro da Unity

        AudioClip -> tipo da váriavel, literalmente, é algum audio que você queira usar
        AudioSource -> Manipula os audios
         
        var.GetComponent<example>(); -> atribui as caracteristicas de example a variavel var
            ex: o componente AudioSource possui um método chama PlayOneShot();

            se fizermos:
                var.GetComponent<AudioSource>();

            var agora pode utilizar PlayOneShot(); :
                var.PlayOneShot();

         */


    public float vol;

    // Audio a ser tocado
    public AudioClip _nome;

    // Controlador do audio
    AudioSource AudioController;

    void Start()
    {
        // Como o AudioController não está definido como public, você precisa manipula-la dentro do script
        // GetComponent encontra um component definido dentro do gameObject e atribui a váriavel escolhida
        AudioController = GetComponent<AudioSource>();
        // Neste caso, GetComponent procura por um AudioSource, e caso encontre, atribui ele a variavel AudioController

        TocarAudio();
    }

    void TocarAudio()
    {
        // Toca o audio "_nome" um vez com volume "vol"
        AudioController.PlayOneShot(_nome, vol);
    }
}
