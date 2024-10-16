using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private float damage = 1f; 
    [SerializeField] private float pushForce = 10f; 
    [SerializeField] private AudioClip DamageSFX;

    private AudioSource audiosource;

    private void OnEnable()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null && player.PlayerProfile != null)
            {

                player.PlayerProfile.Lifes -= (int)damage;
                Debug.Log("Vidas restantes: " + player.PlayerProfile.Lifes);

                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    Vector2 pushDirection = -playerRb.velocity.normalized;
                    playerRb.velocity = pushDirection * pushForce;
                }

                if (!audiosource.isPlaying)
                {
                    audiosource.PlayOneShot(DamageSFX);
                }
            }
        }
    }
}
