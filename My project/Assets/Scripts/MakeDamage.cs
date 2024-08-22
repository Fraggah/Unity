using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    // Variables a configurar desde el editor
    [Header("Setup")]
    [SerializeField] float damage = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(-damage);
            Debug.Log(" PUNTOS DE DAÑO REALIZADOS AL JUGADOR " + damage);
        }
    }
}