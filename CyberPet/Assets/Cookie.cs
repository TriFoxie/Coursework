using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cookie : MonoBehaviour
{
    public Animator animator;
    private bool pressed;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !pressed) 
        {
            pressed = true;
            animator.SetBool("Pressed?", true);
        }
        else
        {
            pressed = false;
            animator.SetBool("Pressed?", false);
        }
    }
}
