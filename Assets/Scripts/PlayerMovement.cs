using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// https://youtu.be/dwcT-Dch0bA
public class PlayerMovement : MonoBehaviour
{
    private float horizontal = 0f;
    private float vertical;
    public CharacterController controller;
    private bool jump = false;
    public Animator animator;
    

    public BoxCollider2D wallClimbCollider;

    public float runSpeed = 40f;
    
    private AudioSource audioSource;

    public AudioClip runSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontal * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    // Called by animation event
    public void PlayWalkSound()
    {
        audioSource.clip = runSound;
        audioSource.pitch = Random.Range(.5f, 1f);
        audioSource.Play();
    }
}
