using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTipsController : MonoBehaviour
{
    public Animator animator;

    public void Next()
    {
        animator.SetBool("fadeOut", true);
    }
    
}
