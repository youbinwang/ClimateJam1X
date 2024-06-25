using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovements : MonoBehaviour
{
    [Header("Character Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravityMultiplier = 2f; // Multiplier for gravity to make the fall faster
    private Rigidbody rb;
    private float moveX = 0f;
    private bool jump = false;
    
    [Header("Ground Check")]
    private bool isGrounded;
    [SerializeField] float groundCheckDistance = 1f;
    [SerializeField] private LayerMask groundLayer; // Assign ground layer to platforms
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        // Ground Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, Color.red);
        
        // Movement
        Vector3 velocity = rb.velocity;
        velocity.x = moveX * moveSpeed;
        rb.velocity = velocity;
        
        // Jump
        if (jump && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = false;
        }
        
        // Extra gravity for a more natural fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * (Physics.gravity.y * (gravityMultiplier - 1) * Time.deltaTime);
        }
    }
}
