using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [SerializeField] private int doorIndex;
    [SerializeField] private int cameraIndex;
    [SerializeField] private int targetDoorID; 
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip enterDoor;

    private bool isNearDoor = false;
    private SpriteRenderer spriteRenderer;  

    private AudioSource audiosource;

    [Header("Sprites")]
    [SerializeField] private Sprite defaultSprite;  
    [SerializeField] private Sprite nearSprite;    

    private void OnEnable()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && defaultSprite != null)
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }

    void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            Door currentDoor = GetComponent<Door>();

            if (currentDoor != null)
            {
                if (currentDoor.isTheLast)
                {
                    TeleportPlayerToDeath();
                }
                else
                {
                    TeleportPlayer();
                }

                PlayAudio(enterDoor);

                SetTargetDoorAsLast();
            }
        }
    }

    private void SetTargetDoorAsLast()
    {
        Door[] allDoors = FindObjectsOfType<Door>();

        foreach (Door door in allDoors)
        {

            if (door.doorID == targetDoorID)
            {
                door.isTheLast = true;
                Debug.Log("Puerta " + door.doorID + " marcada como la última.");
            }
            else
            {
                door.isTheLast = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearDoor = true;

            if (spriteRenderer != null && nearSprite != null)
            {
                spriteRenderer.sprite = nearSprite;
            }

            PlayAudio(openDoor);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearDoor = false;

            if (spriteRenderer != null && defaultSprite != null)
            {
                spriteRenderer.sprite = defaultSprite;
            }
        }
    }

    private void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = GameManager.instance.doorPositions[doorIndex];
            Camera.main.transform.position = GameManager.instance.cameraPositions[cameraIndex];
        }
    }

    private void TeleportPlayerToDeath()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = GameManager.instance.doorPositions[14];  
            Camera.main.transform.position = GameManager.instance.cameraPositions[5]; 
            Debug.Log("Muerte");
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(clip);
        }
    }
}
