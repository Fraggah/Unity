using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float waitingTime = 1f;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float intervalTime = 2f;

    [SerializeField]
    [Range(1f, 30f)]
    private float toggleInterval = 10f;

    [SerializeField]
    private float initialDelay = 3f;

    private bool isSpawning = false;
    private float spawnTimer = 0f;
    private List<GameObject> spawnedPrefabs = new List<GameObject>();

    private bool hasStarted = false;

    void Start()
    {
        spawnTimer = initialDelay;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (!hasStarted && spawnTimer >= initialDelay)
        {
            spawnTimer = 0f;
            hasStarted = true;
        }

        if (hasStarted && spawnTimer >= toggleInterval)
        {
            if (isSpawning)
            {
                CancelInvoke(nameof(SpawnRandom));
                Debug.Log("Spawneo desactivado");
                ClearSpawnedPrefabs();
            }
            else
            {
                InvokeRepeating(nameof(SpawnRandom), waitingTime, intervalTime);
                Debug.Log("Spawneo activado");
            }

            isSpawning = !isSpawning; 
            spawnTimer = 0f;      
        }
    }

    void SpawnRandom()
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject randomPrefab = prefabs[randomIndex];
        GameObject instance = Instantiate(randomPrefab, transform.position, Quaternion.identity);

        spawnedPrefabs.Add(instance);
    }

    private void ClearSpawnedPrefabs()
    {
        foreach (GameObject obj in spawnedPrefabs)
        {
            if (obj != null) Destroy(obj);
        }

        spawnedPrefabs.Clear();
    }
}
