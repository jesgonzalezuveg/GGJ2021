using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampaDetect : MonoBehaviour{

    public bool checaDmg() {

        var cosas = Physics.OverlapBox(transform.position, new Vector3(0.35f, 0.35f, 0.35f));

        foreach (Collider cosa in cosas) {
            Debug.Log(cosa.name);
            if(cosa.gameObject.tag == "Player") {
                Debug.Log("Bajar vida");
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.7f, 0.7f, 0.7f));
    }

}
