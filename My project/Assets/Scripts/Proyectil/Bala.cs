using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bala : MonoBehaviour
    
{
    private void OnEnable()
    {
        Impulse(); 
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }

    protected abstract void Impulse();
}
