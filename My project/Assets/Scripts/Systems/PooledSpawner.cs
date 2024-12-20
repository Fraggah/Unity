using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float waitingTime;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float intervalTime;

    [SerializeField]
    private AudioClip spawnSound;

    private AudioSource audioSource;
    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = GetComponent<ObjectPool>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("falta audiosource");
        }
        if (spawnSound == null)
        {
            Debug.LogWarning("falta sonido");
        }
    }

    void Start()
    {
        InvokeRepeating(nameof(GenerarObjetoLoop), waitingTime, intervalTime);
    }

    void GenerarObjetoLoop()
    {
        GameObject pooledObject = objectPool.GetPooledObject();

        if (pooledObject != null)
        {
            pooledObject.transform.position = transform.position;
            pooledObject.transform.rotation = Quaternion.identity;
            pooledObject.SetActive(true);

            PlaySpawnSound();
        }
    }

    private void PlaySpawnSound()
    {
        if (audioSource != null && spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
    }
}
