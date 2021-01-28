using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float smoothTime = 0.1f;
    public float walkSpeed = 2.5f;
    public float runSpeed = 8f;
    public Transform cameraTransform;

    float horizontal = 0;
    float vertical = 0;
    Vector3 direction;

    public Transform groundCheck;
    public float distanciaCheck = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;
    public float gravity = -9.81f;
    Vector3 fallVelocity;

    public float jumpSpeed = 3f;

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public float smoothTimeVelocity = 0.1f;
    [HideInInspector]
    public CharacterController playerControllerOb;

    public void Awake() {
        playerControllerOb = gameObject.GetComponent<CharacterController>();   
    }

    public void Update() {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0f, vertical);

        isGrounded = Physics.CheckSphere(groundCheck.position, distanciaCheck, groundMask);


        if (direction.magnitude >= 0.1) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTimeVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float realSpeed = walkSpeed;


            if (Input.GetKey(KeyCode.LeftControl)) {
                realSpeed = walkSpeed * 0.5f;
            } else if (Input.GetKey(KeyCode.LeftShift)) {
                realSpeed = runSpeed;
            } else {
            }

            playerControllerOb.Move(moveDirection.normalized * realSpeed * Time.deltaTime);

        } else { 

        }

        if (isGrounded) {
            fallVelocity.y = 0f;
            if (Input.GetKey(KeyCode.Space)) {
                fallVelocity.y = Mathf.Sqrt(jumpSpeed * -2 * gravity);
            }
        }

        fallVelocity.y += gravity * Time.deltaTime;
        playerControllerOb.Move(fallVelocity * Time.deltaTime);
    }
}
