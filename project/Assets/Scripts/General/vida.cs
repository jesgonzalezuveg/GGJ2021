using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida : MonoBehaviour
{
    public float timerTrampa;
    public float tiempoDano=0;
    public int salud = 5; 
    public CharacterController player;
    public GameObject gameOver;
    private void Start() 
    {
        
        player = gameObject.GetComponent<CharacterController>(); 
        gameOver.SetActive(false);
    }
    void Update()
    {
        if (salud==0)
        {
            gameOver.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider objeto) 
    {
        if (objeto.tag=="trampa")
        {
            reloj();
        }
    }
    private void OnTriggerEnter(Collider objeto) 
    {
        timerTrampa=Random.Range(0.5f, 3f);
    }
    private void OnTriggerExit(Collider objeto) 
    {
        
    }
    void reloj()
    {
        if(timerTrampa<=3&&timerTrampa>=0)
        {
            timerTrampa-=Time.deltaTime;   
        }  
        if(timerTrampa<0)
        {
            if(Time.time>=tiempoDano){
            salud=salud-1;
            tiempoDano=Time.time+3;
        }
        }   
        if(timerTrampa<-1)
        {
            timerTrampa=4;
        }  
        
    }
    
}
