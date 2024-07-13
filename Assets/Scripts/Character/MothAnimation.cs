using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("IdleTrigger"); 
        }
    }
}
