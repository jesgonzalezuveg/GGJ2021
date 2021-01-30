using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class primeraPersona : MonoBehaviour{

    public float smoothTime = 0.1f;
    public float velocidad = 3f;
    
    public int runSpeed = 6; 
    public float groundDistance = 0.1f;
    public float jumpSpeed = 3f;
    public float gravity = -9.81f;

    public Transform cameraTransform;    
    public Transform groundCheck;

    public LayerMask groundMask;

    [HideInInspector]
    private Vector3 camaraForward;
    [HideInInspector]
    private Vector3 camaraRight;
    [HideInInspector]
    private Vector3 movPlayer;
    [HideInInspector]
    public int walkSpeed = 1;
    [HideInInspector]
    public bool saltoTierra;
    [HideInInspector]
    float horizontal = 0;
    [HideInInspector]
    float vertical = 0;
    [HideInInspector]
    Vector3 direction;
    [HideInInspector]
    public float smoothTimeVelocity = 0.1f;
    [HideInInspector]
    bool isGrounded;
    [HideInInspector]
    public CharacterController playerController;
    
    public Vector3 fallVelocity;
    [HideInInspector]
    public Animator animatorPlayer;

    public void Awake() {
        playerController = gameObject.GetComponent<CharacterController>();
        animatorPlayer = gameObject.GetComponentInChildren<Animator>();

    }

    public void Update() 
    {
        isGrounded = Physics.CheckBox(groundCheck.position, new Vector3(0.2f, groundDistance,0.2f), Quaternion.identity,groundMask);

        if (isGrounded && fallVelocity.y < 0f) 
        {
            fallVelocity.y = -1f;
        }
        //AD
        horizontal = Input.GetAxis("Horizontal");
        //WS
        vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1) 
        {
            //CAMINAR
            animatorPlayer.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTimeVelocity, smoothTime);        
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            float realSpeed = velocidad;
            //ROTACIONJUGADOR
            playerController.transform.LookAt(playerController.transform.position + moveDirection);

            //CORRER
            if (Input.GetKey(KeyCode.LeftControl)) {
                realSpeed = walkSpeed * 0.5f;
            } else if (Input.GetKey(KeyCode.LeftShift)) {
                realSpeed = walkSpeed+runSpeed;
            } else {
            }
            playerController.Move(moveDirection.normalized * realSpeed * Time.deltaTime);
        //IDLE
        }else
        {
            animatorPlayer.SetBool("isRunning", false);     
        }
        
        if(isGrounded){
            saltoTierra = true;
            animatorPlayer.SetBool("isJumping", false);
            animatorPlayer.SetBool("isFalling", false);
        } else {
            animatorPlayer.SetBool("isJumping", false);
            animatorPlayer.SetBool("isFalling", true);
        }

        //SALTAR
        if (Input.GetKey("space")) {
            
            if(saltoTierra) 
            {
                animatorPlayer.SetBool("isJumping", true);    
                saltoTierra = false;                            
                fallVelocity.y = Mathf.Sqrt(jumpSpeed * -4 * gravity);
            }
        }

        fallVelocity.y += gravity * Time.deltaTime *4;
        playerController.Move(fallVelocity * Time.deltaTime);
        //if (fallVelocity.y<-20){
        //    fallVelocity.y = 0;
        //}
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(groundCheck.position, new Vector3(0.5f, groundDistance, 0.5f));
    }
}


