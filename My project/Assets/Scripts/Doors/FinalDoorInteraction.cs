using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Player playerScript;
    private Collect playerCollectScript;

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
            playerCollectScript = player.GetComponent<Collect>();
        }
    }

    void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.E) && playerScript != null)
        {

            if (playerCollectScript.GetKeys().Count > 0)
            {
                Debug.Log("Ganaste!");
                TeleportPlayer(playerCollectScript.GetKeys()[0]); 
                playerCollectScript.GetKeys().RemoveAt(0); 
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

            if (playerCollectScript.GetKeys().Count > 0)
            {
                if (spriteRenderer != null && nearSprite != null)
                {
                    spriteRenderer.sprite = nearSprite;
                }

                if (playerCollectScript != null && !audiosource.isPlaying)
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

    private void TeleportPlayer(GameObject key)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("NivelBoss");
    }

}
