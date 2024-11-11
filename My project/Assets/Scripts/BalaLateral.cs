using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaLateral : Bala
{
    [SerializeField] private float speed = 5f; 

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Impulse()
    {
        rb.velocity = Vector2.left * speed;
    }
}
