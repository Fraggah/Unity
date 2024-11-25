using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 25; 

    private int currentHealth; 
    private SpriteRenderer spriteRenderer; 

    [SerializeField]
    private AudioClip damageSound; 
    [SerializeField]
    private AudioClip deathSound; 
    [SerializeField]
    private GameObject deathEffect; 

    private AudioSource audioSource; 

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("falta spriterenderer.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("falta audiosource.");
        }

        UpdateColor();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        Debug.Log($"Vida jefe: {currentHealth}");

        PlayDamageSound();

        UpdateColor();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("El jefe ha sido derrotado.");

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);

        SceneManager.LoadScene("Final");
    }

    private void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    private void UpdateColor()
    {
        if (spriteRenderer != null)
        {
            float t = 1f - (float)currentHealth / maxHealth;
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, t);
        }
    }
}
