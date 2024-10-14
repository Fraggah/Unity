using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerProfile", menuName = "SO/Player Profile")]

public class PlayerProfile : ScriptableObject
{
    [SerializeField] private int lifes;
    public int Lifes { get => lifes; set => lifes = value; }

    [SerializeField]
    [Range(.1f, 1f)]
    private float jumpForce;
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    
}
