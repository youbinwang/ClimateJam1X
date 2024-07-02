using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isWalking = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        bool isRunning = isWalking && Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetKeyDown(KeyCode.Space); // Detect jump input

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
        if (isJumping)
        {
            animator.SetTrigger("Jump"); // Assuming "Jump" is the trigger parameter name
        }
    }
}
