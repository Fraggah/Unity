using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
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

        //cambie esa logica por sino al ir a la izquierda antes de volver
        // a iddle se flipeaba por un instante el sprite

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        int velX = (int)rb.velocity.x;
        animator.SetInteger("Velocity", velX);
    }
}
