using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MothAnimation : MonoBehaviour
{
    private Animator animator;
    private int currentAnimationIndex = 1; // Start with IdleAnimation1
    private const float switchInterval = 5f; // Interval between switches in seconds

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SwitchIdleAnimations());
    }

    IEnumerator SwitchIdleAnimations()
    {
        while (true)
        {
            // Toggle between IdleAnimation1 and IdleAnimation2 based on currentAnimationIndex
            if (currentAnimationIndex == 1)
            {
                animator.SetInteger("IdleSwitchCounter", 1); // Set parameter for IdleAnimation1
                Debug.Log("Switching to IdleAnimation1");
                currentAnimationIndex++; // Switch to IdleAnimation2 next
            }
            else if (currentAnimationIndex == 2)
            {
                animator.SetInteger("IdleSwitchCounter", 2); // Set parameter for IdleAnimation2
                Debug.Log("Switching to IdleAnimation2");
                currentAnimationIndex--; // Switch to IdleAnimation1 next
            }

            Debug.Log("Current Animation Index: " + currentAnimationIndex);
            Debug.Log("IdleSwitchCounter: " + animator.GetInteger("IdleSwitchCounter"));

            yield return new WaitForSeconds(switchInterval);
        }
    }
}
