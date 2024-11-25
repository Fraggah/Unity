using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Tilemaps;

public class FallReset : MonoBehaviour
{
    [SerializeField] private PlayableDirector timelineDirector;
    [SerializeField] private float damage = 0f;

    [Header("Tilemap Names")]
    [SerializeField] private string mapObjectName = "---- MAP ----";
    [SerializeField] private string gridChildName = "Grid";
    [SerializeField] private string plataformChildName = "Plataform";
    [SerializeField] private string decorationChildName = "Decoration";

    [Header("Transport Positions")]
    [SerializeField] private Vector3 playerTransportPosition = Vector3.zero;
    [SerializeField] private Vector3 cameraTransportPosition = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null)
            {
                GameManager.Instance.AddLifes(-(int)damage);

                if (GameManager.Instance.PlayerLifes > 0)
                {
                    collision.transform.position = playerTransportPosition;
                    Camera.main.transform.position = cameraTransportPosition;

                    MakeTilemapsVisible();

                    if (timelineDirector != null)
                    {
                        timelineDirector.Play();
                    }
                }
                else
                {
                    Debug.Log("GAME OVER");
                }
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
                    Debug.LogWarning($"faLTA '{plataformChildName}' en '{gridChildName}'.");
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
            Debug.LogWarning($" '{mapObjectName}' no encontrado.");
        }
    }
}
