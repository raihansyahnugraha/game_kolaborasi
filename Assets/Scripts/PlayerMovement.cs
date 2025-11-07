using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpHeight = 3f;
    public float gravity = -20f;
    public KeyCode keyRun = KeyCode.LeftShift;
    
    [Header("Ground Check")]
    public Transform cameraTransform;
    public Transform groundCheck;
    public float radiusRay = 0.3f; 
    public LayerMask groundLayer;
    
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGround;

    private float moveSpeed;
    private float moveX;
    private float moveZ;

    private Animator animator;

    public float distance = 3;
    public float mouseSensitivity = 3;
    private float rotationX;
    private float rotationY;
    private float minY = -20;
    private float maxY = 80;

    private static readonly int _isJump = Animator.StringToHash("isJump");
    private static readonly int _isRunning = Animator.StringToHash("isRunning");
    private static readonly int _isWalking = Animator.StringToHash("isWalking");

    private void Start()
    {
        moveSpeed = walkSpeed;
        controller = GetComponent<CharacterController>();
        animator = FindObjectOfType<Animator>();
    }

    private void Update()
    {
        GroundCheck();  
        Movement();
        Jump();
        Run();
        RotateCamera();
    }

    private void RotateCamera()
    {
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        var playerPosition = transform.position;
        var position = playerPosition - (rotation * Vector3.forward * distance);
        cameraTransform.position = position;
        cameraTransform.LookAt(playerPosition);
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        isGround = Physics.Raycast(groundCheck.position, Vector3.down, out hit, radiusRay, groundLayer);
        
        if (isGround)
        {
            animator.SetBool(_isJump, false);
        }
    }

    private void Run()
    {
        if (Input.GetKey(keyRun))
            {
                moveSpeed = runSpeed;
                animator.SetBool(_isRunning, true);
            }
            else
            {
                moveSpeed = walkSpeed;
                animator.SetBool(_isRunning, false);
            }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            animator.SetBool(_isJump, true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Movement()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveX, 0, moveZ).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);

            animator.SetBool(_isWalking, true);
        }
        else
        {
            animator.SetBool(_isWalking, false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * radiusRay);
    }
}
