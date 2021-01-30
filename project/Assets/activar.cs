using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activar : MonoBehaviour
{
    
    public GameObject[] objetos;
    void Start()
    {
        foreach (var item in objetos)
        {
            item.SetActive(true);
        }
    }

}
