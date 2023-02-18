using System;
using UnityEngine;
using Random = UnityEngine.Random;

// https://youtu.be/dwcT-Dch0bA
public class PlayerMovement : MonoBehaviour
{
    private float _horizontal = 0f;
    private float _vertical;
    public CharacterController controller;
    private bool _jump = false;
    public Animator animator;


    public BoxCollider2D wallClimbCollider;

    public float runSpeed = 40f;

    private AudioSource _audioSource;

    public AudioClip runSound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Math.Abs(_horizontal));

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jump = true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(_horizontal * Time.fixedDeltaTime, false, _jump);
        _jump = false;
    }

    // Called by animation event
    public void PlayWalkSound()
    {
        _audioSource.clip = runSound;
        _audioSource.pitch = Random.Range(.5f, 1f);
        _audioSource.Play();
    }
}