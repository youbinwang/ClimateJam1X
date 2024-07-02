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

            // Check if shift is held down to transition to running
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
    }
}