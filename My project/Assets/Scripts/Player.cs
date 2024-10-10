using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] public float lifes = 3f;
    public bool hadKey = false;

    void Awake()
    {
        Debug.Log("Vidas: " + lifes);
    }

    void Update()
    {
        if(!Alive())
        {
            LooseGame();
        }
    }

    public void TakeDamage(float damage)
    {
        lifes += damage;
    }


    private bool Alive()
    {
        return lifes > 0;
    }

    private void LooseGame()
    {
        gameObject.transform.position = GameManager.instance.doorPositions[16];
        Camera.main.transform.position = GameManager.instance.cameraPositions[7];
        Debug.Log("GAME OVER");
    }
}