using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaraPersonaje : MonoBehaviour
{
    public float speed; 
    [Range (0, 1)]public float lerpValue;
    public Vector3 offset;
    public Transform target;
    [Range (1, 2)]public float sensibilidad;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValue);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibilidad, Vector3.up)* offset;
        transform.LookAt(target);


        if (target) {
            Debug.Log("Player visible: " + target.GetComponentInChildren<Renderer>().isVisible);
        }

    }
}
