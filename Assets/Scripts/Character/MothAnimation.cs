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
            if (currentAnimationIndex == 1)
            {
                animator.SetInteger("IdleSwitchCounter", 1); 
                currentAnimationIndex++; 
            }
            else if (currentAnimationIndex == 2)
            {
                animator.SetInteger("IdleSwitchCounter", 2); 
                currentAnimationIndex--; 
            }

            Debug.Log("Current Animation Index: " + currentAnimationIndex);
            Debug.Log("IdleSwitchCounter: " + animator.GetInteger("IdleSwitchCounter"));

            yield return new WaitForSeconds(switchInterval);
        }
    }
}
