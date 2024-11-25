using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    [Header("Colors")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color lastDoorColor = Color.red;

    [Header("Tilemap Names")]
    [SerializeField] private string mapObjectName = "---- MAP ----";
    [SerializeField] private string gridChildName = "Grid";
    [SerializeField] private string plataformChildName = "Plataform"; 
    [SerializeField] private string decorationChildName = "Decorations";

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
            UpdateDoorColor();
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
                    MakeTilemapsVisible();
                }

                PlayAudio(enterDoor);

                SetTargetDoorAsLast();
            }
        }
    }

    private void MakeTilemapsVisible()
    {
        GameObject mapObject = GameObject.Find(mapObjectName);

        if (mapObject != null)
        {
            Transform gridTransform = mapObject.transform.Find(gridChildName);
            if (gridTransform != null)
            {

                Transform plataformTransform = gridTransform.Find(plataformChildName);
                if (plataformTransform != null)
                {
                    TilemapRenderer plataformRenderer = plataformTransform.GetComponent<TilemapRenderer>();
                    if (plataformRenderer != null)
                    {
                        plataformRenderer.enabled = true;
                    }
                }
                else
                {
                    Debug.LogWarning($"falta '{plataformChildName}'en '{gridChildName}'.");
                }

                Transform decorationTransform = gridTransform.Find(decorationChildName);
                if (decorationTransform != null)
                {
                    TilemapRenderer decorationRenderer = decorationTransform.GetComponent<TilemapRenderer>();
                    if (decorationRenderer != null)
                    {
                        decorationRenderer.enabled = true;
                    }
                }
                else
                {
                    Debug.LogWarning($"falta '{decorationChildName}' en '{gridChildName}'.");
                }
            }
            else
            {
                Debug.LogWarning($"falta '{gridChildName}' en '{mapObjectName}'.");
            }
        }
        else
        {
            Debug.LogWarning($"falta '{mapObjectName}' en.");
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
                Debug.Log("puerta " + door.doorID + " marcada como last.");
            }
            else
            {
                door.isTheLast = false;
            }

            DoorInteraction doorInteraction = door.GetComponent<DoorInteraction>();
            if (doorInteraction != null)
            {
                doorInteraction.UpdateDoorColor();
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
            player.transform.position = GameManager.Instance.doorPositions[doorIndex];
            Camera.main.transform.position = GameManager.Instance.cameraPositions[cameraIndex];
        }
    }

    private void TeleportPlayerToDeath()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            player.transform.position = GameManager.Instance.doorPositions[14];
            Camera.main.transform.position = GameManager.Instance.cameraPositions[5];
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

    public void UpdateDoorColor()
    {
        Door door = GetComponent<Door>();
        if (door != null && spriteRenderer != null)
        {
            spriteRenderer.color = door.isTheLast ? lastDoorColor : defaultColor;
        }
    }
}
