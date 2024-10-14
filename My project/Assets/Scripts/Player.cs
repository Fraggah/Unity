using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private PlayerProfile playerProfile; 
    public PlayerProfile PlayerProfile { get => playerProfile; }

    private int lifes; 

    void Awake()
    {
        lifes = playerProfile.Lifes;
        Debug.Log("Vidas: " + lifes);
    }

    void Update()
    {
        if (!Alive())
        {
            LooseGame();
        }
    }

    public void TakeDamage(int damage)
    {
        lifes -= damage; 
        playerProfile.Lifes = lifes;
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
