using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Transform player;

    [SerializeField]
    private GameObject destructionParticles; 
    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("falta spriterenderer.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.z = 0;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Boss"))
        {
            CreateParticles();
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        CreateParticles();
        Destroy(gameObject);
    }

    private void CreateParticles()
    {
        if (destructionParticles != null)
        {
            GameObject particles = Instantiate(destructionParticles, transform.position, Quaternion.identity);

            var particleSystem = particles.GetComponent<ParticleSystem>();
            if (particleSystem != null && spriteRenderer != null)
            {
                var mainModule = particleSystem.main;
                mainModule.startColor = spriteRenderer.color;
            }
        }
    }
}
