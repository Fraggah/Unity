using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private PlayerProfile playerProfile;
    public PlayerProfile PlayerProfile { get => playerProfile; }

    private float jumpForce;

    [SerializeField] private AudioClip jumpSFX;

    private bool canJump = true;
    private bool jumping = false;

    private Animator animator;
    private Rigidbody2D rb;
    private AudioSource audiosource;

    private void Awake()
    {
        jumpForce = playerProfile.JumpForce;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(jumpSFX);
            }
        }
        animator.SetBool("Jumping", jumping);
    }

    private void FixedUpdate()
    {
        if (!canJump && !jumping)
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