using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 10f; // Speed when running
    public float jumpForce = 5f;
    public float gravityMultiplier = 2f;
    private Rigidbody rb;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
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

        // Debugging: Log the isGrounded state and jump input
        Debug.Log("isGrounded: " + isGrounded);
        Debug.Log("Jump Button Pressed: " + Input.GetButtonDown("Jump"));

        // Apply jump force if the player presses the jump button and the character is on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jumping");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }

        // Apply extra gravity manually for a more natural fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.deltaTime;
        }
    }

    void CheckGrounded()
    {
        // Check if the character is grounded using a small sphere cast
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Debug draw sphere
        Debug.DrawRay(groundCheck.position, Vector3.down * groundCheckRadius, isGrounded ? Color.green : Color.red);
    }
}
