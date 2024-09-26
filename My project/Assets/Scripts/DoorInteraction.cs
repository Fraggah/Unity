using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public int doorIndex;
    public int cameraIndex;
    private bool isNearDoor = false;

    void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            TeleportPlayer(); // Teletransporta al jugador y la cámara a la posición de destino
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearDoor = true;
        }
        Debug.Log("Presiona E para interactuar");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearDoor = false;
        }
    }

    private void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = GameManager.instance.doorPositions[doorIndex]; // Mueve al jugador a la posición de destino

            // También puedes mover la cámara si lo necesitas
            Camera.main.transform.position = GameManager.instance.cameraPositions[cameraIndex];
        }
    }
}