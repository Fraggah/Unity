using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] public float lifes = 3f;

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
        Camera.main.transform.position = GameManager.instance.cameraPositions[3];
        Debug.Log("GAME OVER");
    }
}