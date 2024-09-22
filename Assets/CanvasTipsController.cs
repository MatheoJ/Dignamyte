using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTipsController : MonoBehaviour
{
    public Animator animator;

    private bool isStopped = false;
    public void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("CanvaTipsIddle") && !isStopped)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().StopGame();
            isStopped = true;
        }
    }

    public void Next()
    {
        animator.SetBool("fadeOut", true);
    }
    
}
