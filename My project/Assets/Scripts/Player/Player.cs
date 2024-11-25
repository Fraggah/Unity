using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private PlayerProfile playerProfile;
    [SerializeField] private float pushForce = 10f;
    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private UnityEvent<int> OnLivesChanged;

    private AudioSource audioSource;
    private bool isGameOver = false;
    private float lastDamageTime;

    [Header("Invulnerability")]
    [SerializeField] private float invulnerabilityTime = 0.5f;

    [Header("Collision Settings")]
    [SerializeField] private LayerMask enemyLayerMask;

    private Collider2D playerCollider;

    [Header("Tilemap Names")]
    [SerializeField] private string mapObjectName = "---- MAP ----";
    [SerializeField] private string gridChildName = "Grid";
    [SerializeField] private string plataformChildName = "Plataform";
    [SerializeField] private string decorationChildName = "Decorations";

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerCollider = GetComponent<Collider2D>();

        if (GameManager.Instance != null)
        {
            int currentLives = GameManager.Instance.GetLifes();
            OnLivesChanged.Invoke(currentLives);
            Debug.Log("vidas iniciales: " + currentLives);
        }
    }

    private void Update()
    {
        if (!Alive() && !isGameOver)
        {
            GameOver();
        }
    }

    public void TakeDamage(int damage, Vector2 pushDirection)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddLifes(-damage);
            Debug.Log($"vidas: {GameManager.Instance.GetLifes()}");
            OnLivesChanged.Invoke(GameManager.Instance.GetLifes());
        }

        ApplyKnockback(pushDirection);

        if (damageSFX != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(damageSFX);
        }

        if (GameManager.Instance.PlayerLifes <= 0 && !isGameOver)
        {
            GameOver();
            GameManager.Instance.ResetLifes();
        }

        StartCoroutine(TemporaryInvulnerability());
    }

    private void ApplyKnockback(Vector2 pushDirection)
    {
        Rigidbody2D playerRb = GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = pushDirection.normalized * pushForce;
        }
    }

    private IEnumerator TemporaryInvulnerability()
    {
        IgnoreEnemyCollisions(true);
        yield return new WaitForSeconds(invulnerabilityTime);
        IgnoreEnemyCollisions(false);
    }

    private void IgnoreEnemyCollisions(bool ignore)
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int bossLayer = LayerMask.NameToLayer("Boss");

        if (enemyLayer != -1 && bossLayer != -1)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, ignore);
            Physics2D.IgnoreLayerCollision(gameObject.layer, bossLayer, ignore);
        }
        else
        {
            Debug.LogError("Capaz no definidas");
        }
    }

    public void AddLife(int amount)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddLifes(amount);
            Debug.Log($"+vida: {GameManager.Instance.PlayerLifes}");
            OnLivesChanged.Invoke(GameManager.Instance.PlayerLifes);
        }
    }

    private bool Alive()
    {
        return GameManager.Instance != null && GameManager.Instance.PlayerLifes > 0;
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        isGameOver = true;
        SceneManager.LoadScene("Menu");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            if (Time.time - lastDamageTime >= invulnerabilityTime)
            {
                Vector2 pushDirection = collision.contacts[0].point - (Vector2)transform.position;
                TakeDamage(1, -pushDirection);
                MakeTilemapsVisible();
                lastDamageTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (Time.time - lastDamageTime >= invulnerabilityTime)
            {
                TakeDamage(1, Vector2.zero);
                lastDamageTime = Time.time;
            }
        }
    }

    private void MakeTilemapsVisible()
    {
        GameObject mapObject = GameObject.Find(mapObjectName);

        if (mapObject != null)
        {
            Transform gridTransform = mapObject.transform.Find(gridChildName);

            if (gridTransform != null)
            {
                EnableTilemap(gridTransform, plataformChildName);
                EnableTilemap(gridTransform, decorationChildName);
            }
        }
    }

    private void EnableTilemap(Transform parent, string childName)
    {
        Transform childTransform = parent.Find(childName);
        if (childTransform != null)
        {
            TilemapRenderer renderer = childTransform.GetComponent<TilemapRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }
        }
    }
}
