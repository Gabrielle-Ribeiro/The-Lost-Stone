using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource efxSource; //efeitos sonoros
    public AudioSource musicSource; //musica de fundo
    public static SoundManager instance = null; //unica instancia, uma coisa só cuidando do som, para quando mudar de fase o som continue tocando
    public float lowPitchRange = .95f; //menor volume -- Pitch mais baixo
    public float highPitchRange = 1.05f; //maior volume -- Pitch mais alto

    void Awake()
    {
        if (instance == null)//iniciou-se pela primeira vez no jogo 
        {
            instance = this;
        }
        else if (instance != this) //se for diferente de 'this', deve-se ser destruido
        {
            Destroy(gameObject); 
        }
        DontDestroyOnLoad(gameObject);// cada vez que carregar um cenario novo ele não vai destruir o objeto 

    }
    public void PlaySingle(AudioClip clip) //
    {
        efxSource.clip = clip; //tocar um efeito de cada vez 
        efxSource.Play();
    }
    public void RandomizeSfx(params AudioClip[] clips) //usar audios diferentes para cada ação
    {
        int randomIndex = Random.Range(0, clips.Length); // escolher um clipe 
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);//
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
}

