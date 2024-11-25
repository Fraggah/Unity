using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField] private List<GameObject> keys;
    [SerializeField] private GameObject bag;
    [SerializeField] private AudioClip keySound;
    [SerializeField] private AudioClip positiveLifeSound;
    [SerializeField] private AudioClip negativeLifeSound;
    [SerializeField] private GameObject particleEffectPrefab;

    private AudioSource audioSource;

    private void Awake()
    {
        keys = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            HandleCollectable(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Life"))
        {
            HandleLifePickup(collision.gameObject);
        }
    }

    private void HandleCollectable(GameObject collectable)
    {
        SpawnParticleEffect(collectable);

        collectable.SetActive(false);
        keys.Add(collectable);
        collectable.transform.SetParent(bag.transform);

        PlayKeySound();
    }

    private void HandleLifePickup(GameObject lifePickup)
    {

        Life life = lifePickup.GetComponent<Life>();
        if (life != null)
        {
            int lifeAmount = life.LifeAmount;

            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.AddLife(lifeAmount);
            }

            PlayLifeSound(lifeAmount);
        }

        SpawnParticleEffect(lifePickup);
        lifePickup.SetActive(false);
    }

    private void SpawnParticleEffect(GameObject obj)
    {
        if (particleEffectPrefab != null)
        {
            GameObject particleEffect = Instantiate(particleEffectPrefab, obj.transform.position, Quaternion.identity);
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color objectColor = spriteRenderer.color;
                ParticleSystem particleSystem = particleEffect.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    var mainModule = particleSystem.main;
                    mainModule.startColor = objectColor;
                }
            }
        }
    }

    private void PlayKeySound()
    {
        if (audioSource != null && keySound != null)
        {
            audioSource.PlayOneShot(keySound);
        }
    }

    private void PlayLifeSound(int lifeAmount)
    {
        if (audioSource != null)
        {
            AudioClip clipToPlay = lifeAmount > 0 ? positiveLifeSound : negativeLifeSound;
            if (clipToPlay != null)
            {
                audioSource.PlayOneShot(clipToPlay);
            }
        }
    }

    public List<GameObject> GetKeys()
    {
        return keys;
    }
}
