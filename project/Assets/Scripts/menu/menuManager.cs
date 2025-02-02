using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public string cambioEscena;

    public GameObject transicion;
    public GameObject creditos;
    public GameObject botonesMenu;

    public Animator animatorPlayer;

    private void Awake() {
        transicion.gameObject.SetActive(false);
        creditos.SetActive(false);
        botonesMenu.SetActive(true);        
    }
    
    public void playBtn(){
        transicion.gameObject.SetActive(true);
        StartCoroutine(cargarNivelEnum());
    }

    public void irAMenu(){
        animatorPlayer.SetBool("isBacking", true);
        StartCoroutine(regresar());
    }

    public void irCreditos()
    {
        creditos.SetActive(true);
        botonesMenu.SetActive(false);
    }

    public void quitBtn()
    {
        Application.Quit();
    }

    IEnumerator cargarNivelEnum(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync(cambioEscena);
    }
    IEnumerator regresar(){
        yield return new WaitForSeconds(1);
        
        creditos.SetActive(false);
        botonesMenu.SetActive(true);  
    }
}
