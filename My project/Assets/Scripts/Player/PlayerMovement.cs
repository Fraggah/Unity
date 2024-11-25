using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            sr.flipX = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            sr.flipX = false;
        }

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        int velX = (int)rb.velocity.x;
        animator.SetInteger("Velocity", velX);
    }
}
