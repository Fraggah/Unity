using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalDoorInteraction : MonoBehaviour
{

    [SerializeField] private AudioClip doorLocked;
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip enterDoor;

    private bool isNearDoor = false;
    private SpriteRenderer spriteRenderer; 
    private AudioSource audiosource;

    [Header("Sprites")]
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite nearSprite;   

    private GameObject player;
    private Player playerScript; //para obatener acceso a el script que tiene el bool hadKey

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

        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
    }

    void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.E) && playerScript != null)
        {
            if (playerScript.hadKey)
            {
                Debug.Log("Ganaste!");
                TeleportPlayer();
                if (!audiosource.isPlaying)
                {
                    audiosource.PlayOneShot(enterDoor);
                }
            }
            else
            {
                Debug.Log("Te falta una Llave!");
                if (!audiosource.isPlaying)
                {
                    audiosource.PlayOneShot(doorLocked);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearDoor = true;

            if (playerScript.hadKey)
            {
                if (spriteRenderer != null && nearSprite != null)
                {
                    spriteRenderer.sprite = nearSprite;
                }

                if (playerScript != null && !audiosource.isPlaying)
                {
                    audiosource.PlayOneShot(openDoor);
                }
            }
            Debug.Log("Presiona E para interactuar");
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
            player.transform.position = GameManager.instance.doorPositions[15];
            Camera.main.transform.position = GameManager.instance.cameraPositions[6];
        }
    }
}
