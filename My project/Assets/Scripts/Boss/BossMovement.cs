using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float baseSpeed = 5f;  
    private float currentSpeed;  

    [SerializeField]
    private Boss boss;           

    public Transform player;  

    private void Start()
    {        
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (boss == null)
        {
            boss = GetComponent<Boss>();
            if (boss == null)
            {
                Debug.LogError("falta script de mov.");
            }
        }

        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);
        }

        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        if (boss != null)
        {
            float speedMultiplier = 1f + (1f - (float)boss.CurrentHealth / boss.MaxHealth);
            currentSpeed = baseSpeed * speedMultiplier;

            Debug.Log($"Velocidad actual del boss: {currentSpeed}");
        }
    }
}
