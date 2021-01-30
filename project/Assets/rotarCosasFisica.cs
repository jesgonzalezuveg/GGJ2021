using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotarCosasFisica : MonoBehaviour{

    Rigidbody rgObjeto;

    public float velocidad = 1;

    // Start is called before the first frame update
    void Start(){
        rgObjeto = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        rgObjeto.angularVelocity = new Vector3(0, velocidad, 0);
    }
}
