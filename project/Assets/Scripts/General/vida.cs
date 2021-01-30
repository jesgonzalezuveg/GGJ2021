using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida : MonoBehaviour
{
    public float timerTrampa;
    public float tiempoDano=0;
    public int salud = 5; 
    public AudioClip sonidoTrampa;
    private AudioSource reproductor;
    public GameObject gameOver;
    private getVolumenHexagono generador;

    private void Start() 
    {
        reproductor = gameObject.AddComponent<AudioSource>();
        reproductor.playOnAwake=false;
        reproductor.clip=sonidoTrampa; 
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);
    }
    void Update()
    {
        if (generador==null)
        {
            generador = GameObject.FindObjectOfType<getVolumenHexagono>();
        }

        if (salud==0)
        {
            gameOver.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider objeto) 
    {
        if (objeto.tag=="trampa")
        {
            
            var sonido = objeto.GetComponentInChildren<AudioSource>();
            
            var animatorTrampa1 = objeto.GetComponentsInChildren<Animator>();
            foreach (var item in animatorTrampa1)
            {
                item.SetBool("itsATrap", true);
            }
            var colision = objeto.GetComponentsInChildren<Collider>();
            reloj(colision, sonido);
            
        }
    }
    private void OnTriggerEnter(Collider objeto) 
    {
        if (objeto.tag=="trampa")
        {
            reproductor.Play();
        }
        timerTrampa=1;
        if (objeto.tag=="sueloMuerte")
        {
            
            salud=salud-1;
            transform.position = generador.spawnPoint;
            tiempoDano=Time.time+1;
        }
    }

    private void OnTriggerExit(Collider objeto) 
    {
        timerTrampa=1;
    }
    //CONTADORTRAMPA
    void reloj(Collider[] colision, AudioSource sonido)
    {
        if(timerTrampa<=1&&timerTrampa>=0)
        {
            timerTrampa-=Time.deltaTime;   
        }  
        if(timerTrampa<0)
        {
            if(Time.time>=tiempoDano){
            
            
            salud=salud-1;
            transform.position = generador.spawnPoint;
            tiempoDano=Time.time+1;
            reproductor.Pause();
            sonido.Play();
            
            foreach (var item in colision)
            {
                if (item.isTrigger)
                {
                    item.enabled=false;
                }
            }
        }
        }   
        if(timerTrampa<-1)
        {
            timerTrampa=4;
        }  
        
    }
    
}
