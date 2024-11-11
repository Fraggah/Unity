using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    // Método que se llama al detectar una colisión
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruye el objeto cuando colisiona con cualquier cosa
        Destroy(gameObject);
    }
}