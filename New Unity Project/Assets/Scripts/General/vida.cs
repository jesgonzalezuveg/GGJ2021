using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vida : MonoBehaviour
{
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
    private void OnTriggerEnter(Collider objeto) 
    {
        if (objeto.tag=="trampa")
        {
            salud = salud- 1;
        }
    }
    
}
