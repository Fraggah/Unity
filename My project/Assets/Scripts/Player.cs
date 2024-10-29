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
    private bool isGameOver = false;

    void Awake()
    {
        lifes = playerProfile.Lifes;
        Debug.Log("Vidas: " + lifes);
    }

    void Update()
    {
        if (!Alive() && !isGameOver)
        {
            LooseGame();
            isGameOver = true; 
        }
    }

    public void TakeDamage(int damage)
    {
        lifes -= Mathf.Abs(damage);  
        playerProfile.Lifes = lifes;

        Debug.Log("Vidas: " + lifes);

        if (lifes <= 0 && !isGameOver)
        {
            LooseGame();
            isGameOver = true;
        }
    }

    private bool Alive()
    {
        return playerProfile.Lifes > 0;
    }

    private void LooseGame()
    {
        gameObject.transform.position = GameManager.instance.doorPositions[16];
        Camera.main.transform.position = GameManager.instance.cameraPositions[7];
        Debug.Log("GAME OVER");
    }
}
