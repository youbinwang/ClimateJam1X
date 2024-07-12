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

            // Set animation parameters based on movement states
            if (isRunning)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Stop", false);
                animator.SetBool("Run", true);
            }
            else if (isWalking)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Stop", false);
                animator.SetBool("Run", false);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    animator.SetBool("Run", true);
                }
                else
                {
                    animator.SetBool("Run", false);
                }
            }
            else
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Stop", true);
                animator.SetBool("Run", false);
            }

            // Trigger jump animation
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Jump"); // Assuming "Jump" is the trigger parameter name
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
    }
}
