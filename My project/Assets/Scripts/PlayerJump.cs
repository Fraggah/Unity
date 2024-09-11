using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float jumpForce = 50f;

    [SerializeField] private AudioClip jumpSFX;

    private bool canJump = true;
    private bool jumping = false;

    private Animator animator;
    private Rigidbody2D rb;
    private AudioSource audiosource;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& canJump)
        {
            canJump = false;
            if (audiosource.isPlaying) { return; }
            audiosource.PlayOneShot(jumpSFX);
        }
        animator.SetBool("Jumping", jumping);
    }

    private void FixedUpdate()
    {
        if(!canJump && !jumping) 
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
        jumping = false;
    }

}
