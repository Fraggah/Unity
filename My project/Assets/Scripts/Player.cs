using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private float lifes = 5f;

    public void TakeDamage(float damage)
    {
        lifes += damage;
        Debug.Log(Alive());
    }


    private bool Alive()
    {
        return lifes > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Finish")) { return; }

        Debug.Log("Win");
    }
}