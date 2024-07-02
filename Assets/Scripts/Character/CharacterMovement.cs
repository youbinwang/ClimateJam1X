using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 10f; // Speed when running
    public float jumpForce = 5f;
    private float gravityMultiplier = 2.75f;
    
    private Rigidbody rb;
    private bool isFlipping = false;
    public bool canMove = true;
    
    [Header("Ground Check")]
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    private float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    [Header("Turn")]
    private bool facingRight = true;
    private float turnSpeed = 3.5f;
    [SerializeField] private Transform playerMesh;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }

        CheckGrounded();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");

        // Determine current speed based on whether the run button (Shift) is held down
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        // Apply movement in the X direction only
        Vector3 movement = new Vector3(moveX * currentSpeed, rb.velocity.y, 0);
        rb.velocity = movement;

        // Apply jump force if the player presses the jump button and the character is on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }

        // Apply extra gravity manually for a more natural fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * (Physics.gravity.y * (gravityMultiplier - 1) * Time.deltaTime);
        }
        
        // Player movement direction
        if (moveX > 0 && !facingRight && !isFlipping)
        {
            StartCoroutine(Flip(Vector3.up * 270f));
            facingRight = true;
        }
        else if (moveX < 0 && facingRight && !isFlipping)
        {
            StartCoroutine(Flip(Vector3.up * -270f));
            facingRight = false;
        }
    }

    void CheckGrounded()
    {
        // Check if the character is grounded using a small sphere cast
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Debug draw sphere
        Debug.DrawRay(groundCheck.position, Vector3.down * groundCheckRadius, isGrounded ? Color.green : Color.red);
    }
    
    IEnumerator Flip(Vector3 byAngles)
    {
        isFlipping = true;
        Quaternion fromAngle = playerMesh.rotation;
        Quaternion toAngle = Quaternion.Euler(playerMesh.eulerAngles + byAngles);
        for (float t = 0f; t < 1f; t += Time.deltaTime * turnSpeed)
        {
            playerMesh.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
        playerMesh.rotation = toAngle;
        isFlipping = false;
    }
    
    public void InteractStopMove()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
