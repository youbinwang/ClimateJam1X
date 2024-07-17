using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator animator;
    public bool isInteracting = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isInteracting)
        {
            bool isWalking = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) ||
                             Input.GetKey(KeyCode.RightArrow);
            bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
            bool isClimbing = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || 
                              Input.GetKey(KeyCode.DownArrow);

            if (isClimbing)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Stop", false);
                animator.SetBool("Run", false);
                animator.SetBool("Climb", true);
            }
            else if (isRunning)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Stop", false);
                animator.SetBool("Run", true);
                animator.SetBool("Climb", false);
            }
            else if (isWalking)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Stop", false);
                animator.SetBool("Run", false);
                animator.SetBool("Climb", false);
            }
            else
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Stop", true);
                animator.SetBool("Run", false);
                animator.SetBool("Climb", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Jump"); 
            }
        }
    }

    public void Interact()
    {
        isInteracting = true;
        animator.SetTrigger("Interact");
    }

    public void EndInteraction()
    {
        isInteracting = false;
    }

    public void SetIdleState()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Stop", true);
        animator.SetBool("Run", false);
        animator.SetBool("Climb", false);
    }
}