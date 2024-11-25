using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [Header("Life Settings")]
    [SerializeField] private int lifeAmount = 1;

    public int LifeAmount => lifeAmount;
}
