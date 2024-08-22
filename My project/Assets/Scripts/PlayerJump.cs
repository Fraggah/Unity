using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float jumpForce = 5f;

    private bool canJump = true;
    private bool jumping = false;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& canJump)
        {
            canJump = false;
        }
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
