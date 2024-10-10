using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variable estática para mantener la instancia única
    public static GameManager instance;

    public Vector3[] cameraPositions;
    public Vector3[] doorPositions;
    public Vector3 initialPlayerPosition;

    private void Awake() //asistido por GPT
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