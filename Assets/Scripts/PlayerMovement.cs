using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://youtu.be/dwcT-Dch0bA
public class PlayerMovement : MonoBehaviour
{
    private float horizontal = 0f;
    private float vertical;
    public CharacterController controller;
    private bool jump = false;
    public Animator animator;

    public float runSpeed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    
    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Math.Abs(horizontal));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontal * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
