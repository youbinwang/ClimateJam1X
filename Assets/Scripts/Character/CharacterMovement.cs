using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 10f; 
    public float jumpForce = 5f;
    private float gravityMultiplier = 2.75f;
    
    private Rigidbody rb;
    private bool isFlipping = false;
    [HideInInspector] public bool canMove = true;
    
    [Header("Ground Check")]
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    private float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    
    [Header("Turn")]
    private bool facingRight = true;
    private float turnSpeed = 3.5f;
    [SerializeField] private Transform playerMesh;
    
    [Header("Climb")]
    public LayerMask climbableLayer;
    private bool isClimbing = false;
    public float climbSpeed = 3f;
    private RaycastHit climbHit;
    private bool canStartClimbing = false; // Flag to start climbing

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
        CheckClimbing();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;

        Vector3 movement = new Vector3(moveX * currentSpeed, rb.velocity.y, 0);

        rb.velocity = movement;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            FindObjectOfType<AudioManager>().Play("Jumping");
        }

        if (rb.velocity.y < 0 && !isClimbing)
        {
            rb.velocity += Vector3.up * (Physics.gravity.y * (gravityMultiplier - 1) * Time.deltaTime);
        }
        
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

        if (Input.GetAxis("Vertical") != 0 && !isClimbing)
        {
            canStartClimbing = true;
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            FindObjectOfType<AudioManager>().Play("Landed");
        }

        Debug.DrawRay(groundCheck.position, Vector3.down * groundCheckRadius, isGrounded ? Color.green : Color.red);
    }

    void CheckClimbing()
    {
        if (canStartClimbing)
        {
            RaycastHit hit;
            Vector3 direction = facingRight ? Vector3.right : Vector3.left;

            if (Physics.Raycast(transform.position, direction, out hit, 1f, climbableLayer))
            {
                isClimbing = true;
                climbHit = hit;
            }
            else
            {
                isClimbing = false;
            }

            canStartClimbing = false; // Reset flag after checking
        }

        if (isClimbing)
        {
            RaycastHit surfaceCheck;
            Vector3 climbDirection = (facingRight) ? Vector3.right : Vector3.left;

            if (!Physics.Raycast(transform.position, climbDirection, out surfaceCheck, 1f, climbableLayer))
            {
                isClimbing = false;
                rb.velocity = new Vector3(rb.velocity.x, 0f, 0f); 
                return;
            }

            float climbDirectionInput = Input.GetAxis("Vertical");
            Vector3 climbVelocity = Vector3.up * climbDirectionInput * climbSpeed;

            rb.velocity = new Vector3(rb.velocity.x, climbVelocity.y, 0);
        }
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