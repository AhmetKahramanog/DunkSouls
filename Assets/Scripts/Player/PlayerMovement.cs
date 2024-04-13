using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    private Rigidbody playerRB;
    private Vector3 movement;
    [SerializeField] private Transform cam;
    [SerializeField] private float rotationSpeed;
    private Animator animator;
    [SerializeField] private float jumpForce;
    private bool isGrounded;
    private Vector3 targetDirection;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (!PlayerAttack.isAttack && !PlayerDash.isDashing)
        {
            HandleMovement();
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump(jumpForce);
        }
    }

    private void FixedUpdate()
    {
        HandleRotation();
    }

    private void HandleMovement()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        var movementDirection = cam.transform.forward * movement.z;
        movementDirection += cam.transform.right * movement.x;
        if (movement != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            if (isGrounded)
            {
                animator.SetBool("isJumping", false);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        movementDirection.y = 0f;
        transform.Translate(currentSpeed * Time.deltaTime * movementDirection,Space.World);
    }
    
    private void HandleRotation()
    {
        targetDirection = Vector3.zero;

        targetDirection = cam.transform.forward * movement.z;
        targetDirection += cam.transform.right * movement.x;
        targetDirection.Normalize();
        targetDirection.y = 0f;
        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }
        var target = Quaternion.LookRotation(targetDirection);
        var targetSlerp = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.fixedDeltaTime);
        transform.rotation = targetSlerp;
    }

    private void Jump(float force)
    {
        playerRB.velocity = new Vector3(0f,force, 0f);
        animator.SetBool("isJumping", true);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
