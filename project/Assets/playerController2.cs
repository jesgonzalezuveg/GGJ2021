using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController2 : MonoBehaviour{


    public Rigidbody rgPlayer;

    public float speed = 0.1f;
    public float jumpSpeed = 0.1f;

    public Transform cameraTransform;

    public Transform groundCheck;
    public LayerMask groundMask;

    public bool isGrounded;

    public Animator animatorPlayer;

    public float smoothTime = 0.1f;

    public float smoothTimeVelocity = 0.1f;

    public bool detenedor;

    private void Start() {
        rgPlayer = gameObject.GetComponent<Rigidbody>();
        animatorPlayer = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update(){

        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(0.2f, 0.1f, 0.2f), Quaternion.identity, groundMask);

        rgPlayer.angularVelocity = Vector3.zero;

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontal, 0f, vertical);
        var velocidadActual = rgPlayer.velocity;

        if (direction.magnitude >= 0.1) {

            detenedor = true;

            //Rotar personaje a donde voltea la camara
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTimeVelocity, smoothTime);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.LookAt(transform.position + moveDirection);

            animatorPlayer.SetBool("isRunning", true);
            var velMovimiento = speed;
            if (Input.GetKey(KeyCode.LeftShift)) {
                velMovimiento = speed + 1.5f;
            }
            rgPlayer.velocity = new Vector3(moveDirection.x * velMovimiento, velocidadActual.y, moveDirection.z * velMovimiento) ;
        } else {
            if (detenedor) {
                detenedor = false;
                rgPlayer.velocity = new Vector3(0, velocidadActual.y, 0);
            }
            animatorPlayer.SetBool("isRunning", false);
        }

        if (isGrounded) {
            animatorPlayer.SetBool("isFalling", false);
        }

        if (Input.GetKey("space") && isGrounded) {
            rgPlayer.velocity = new Vector3(velocidadActual.x, jumpSpeed, velocidadActual.z);
            animatorPlayer.SetBool("isJumping", true);
        } else {
            animatorPlayer.SetBool("isJumping", false);
            if (isGrounded) {
                animatorPlayer.SetBool("isFalling", false);
            } else {
                animatorPlayer.SetBool("isFalling", true);
            }
            
        }

    }

}
