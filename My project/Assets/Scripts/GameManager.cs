using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variable est�tica para mantener la instancia �nica
    public static GameManager instance;

    // Un array de posiciones para la c�mara que puedes editar en el Inspector
    public Vector3[] cameraPositions;
    public Vector3[] doorPositions;
    public Vector3 initialPlayerPosition;

    private void Awake()
    {
        // Asegurarse de que solo haya una instancia de GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Hace que el GameManager persista entre escenas
        }
        else
        {
            Destroy(gameObject);  // Elimina instancias duplicadas
        }
    }
}