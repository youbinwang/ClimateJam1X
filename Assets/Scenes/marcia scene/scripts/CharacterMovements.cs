using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravityMultiplier = 2f; // Multiplier for gravity to make the fall faster
    private Rigidbody rb;
    private bool isGrounded;
    public Transform groundCheck; // Create an empty object as ground check
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Assign ground layer to platforms

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

        // movement in the X direction only
        Vector3 movement = new Vector3(moveX * moveSpeed, rb.velocity.y, 0);
        rb.velocity = movement;

        // apply jump force if the player presses the jump button and the character is on the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
        }

        // apply extra gravity for a more natural fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (gravityMultiplier - 1) * Time.deltaTime;
        }
    }

    void CheckGrounded()
    {
        // check if the character is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
