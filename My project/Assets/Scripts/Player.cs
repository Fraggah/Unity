using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private PlayerProfile playerProfile;
    public PlayerProfile PlayerProfile { get => playerProfile; }

    private bool isGameOver = false;

    [SerializeField]
    private UnityEvent<int> OnLivesChanged;

    private void Start()
    {
        OnLivesChanged.Invoke(playerProfile.Lifes);
        Debug.Log("Vidas: " + playerProfile.Lifes);
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
        playerProfile.Lifes = playerProfile.Lifes - damage;

        Debug.Log("Vidas: " + playerProfile.Lifes);

        OnLivesChanged.Invoke(playerProfile.Lifes);

        if (playerProfile.Lifes <= 0 && !isGameOver)
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
