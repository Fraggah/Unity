using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private PlayerProfile playerProfile;
    public PlayerProfile PlayerProfile { get => playerProfile; }

    [SerializeField] private float pushForce = 10f;
    [SerializeField] private AudioClip damageSFX;
    private AudioSource audioSource;

    private bool isGameOver = false;
    [SerializeField] private UnityEvent<int> OnLivesChanged;

    private float invulnerabilityTime = 0.5f;
    private float lastDamageTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        OnLivesChanged.Invoke(playerProfile.Lifes);
        Debug.Log("Vidas: " + playerProfile.Lifes);
    }

    private void Update()
    {
        if (!Alive() && !isGameOver)
        {
            LooseGame();
            isGameOver = true;
        }
    }

    public void TakeDamage(int damage, Vector2 pushDirection)
    {
        playerProfile.Lifes -= damage;
        Debug.Log("Vidas: " + playerProfile.Lifes);

        OnLivesChanged.Invoke(playerProfile.Lifes);

        Rigidbody2D playerRb = GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = pushDirection.normalized * pushForce;
        }

        if (damageSFX != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(damageSFX);
        }

        if (playerProfile.Lifes <= 0 && !isGameOver)
        {
            LooseGame();
            isGameOver = true;
        }
    }

    private bool Alive()
    {
        return playerProfile.Lifes > 0;
    }

    private void LooseGame()
    {
        gameObject.transform.position = GameManager.instance.doorPositions[16];
        Camera.main.transform.position = GameManager.instance.cameraPositions[7];
        Debug.Log("GAME OVER");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (Time.time - lastDamageTime >= invulnerabilityTime)
            {
                int damage = 1;
                Vector2 pushDirection = collision.contacts[0].point - (Vector2)transform.position;
                TakeDamage(damage, -pushDirection);

                lastDamageTime = Time.time;
            }
        }
    }
}
