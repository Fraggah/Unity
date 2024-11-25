using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerProfile", menuName = "SO/Player Profile")]
public class PlayerProfile : ScriptableObject
{
    [Header("Player Stats")]
    [SerializeField] private int initialLifes = 5;  
    [SerializeField] private float initialJumpForce = 0.8f; 

    private int lifes;
    public int Lifes
    {
        get => lifes;
        set => lifes = value;
    }

    private float jumpForce;
    public float JumpForce
    {
        get => jumpForce;
        set => jumpForce = value;
    }

    public void ResetValues()
    {
        lifes = initialLifes;
        jumpForce = initialJumpForce;
    }

    private void OnEnable()
    {
        ResetValues();
    }
}
