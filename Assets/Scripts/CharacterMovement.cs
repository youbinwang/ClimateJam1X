using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Character Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private float moveX = 0f;
    private bool jump = false;
    
    [Header("Ground Check")]
    public float groundCheckDistance = 1f;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;
    
    
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }
    
    void Update()
    { 
        // Horizontal Input
        moveX = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // Ground Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
        
        // Left and Right Movement
        Vector3 velocity = rb.velocity;
        velocity.x = moveX * moveSpeed;
        rb.velocity = velocity;

        // Jump
        if (jump && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }
        
    }
}
