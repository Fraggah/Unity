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

    void Start()
    {
        //InvokeRepeating(nameof(SpawnRandom), waitingTime, intervalTime);
    }

    void SpawnRandom()
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject randomPrefab = prefabs[randomIndex];
        Instantiate(randomPrefab, transform.position, Quaternion.identity);
    }

    private void OnBecameInvisible()
    {
        CancelInvoke(nameof(SpawnRandom));
    }

    private void OnBecameVisible()
    {
        InvokeRepeating(nameof(SpawnRandom), waitingTime, intervalTime);
    }
}
