using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; // Importar la biblioteca para usar Timeline

public class FallReset : MonoBehaviour
{
    [SerializeField] private PlayableDirector timelineDirector; // Referencia al PlayableDirector

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Restablecer la posici�n del jugador
            player.transform.position = GameManager.instance.initialPlayerPosition;

            // Restablecer la posici�n de la c�mara
            Camera.main.transform.position = GameManager.instance.cameraPositions[0];

            // Reproducir el Timeline si est� asignado
            if (timelineDirector != null)
            {
                timelineDirector.Play();
            }
            else
            {
                Debug.LogWarning("No se ha asignado un PlayableDirector al script.");
            }
        }
    }
}
