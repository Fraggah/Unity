using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Darker : MonoBehaviour
{
    [SerializeField] private string mapObjectName = "---- MAP ----"; 
    [SerializeField] private string gridChildName = "Grid"; 
    [SerializeField] private string plataformChildName = "Plataform";
    [SerializeField] private string decorationChildName = "Decorations";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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
                            plataformRenderer.enabled = false;
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"No esta el child '{plataformChildName}' en '{gridChildName}'.");
                    }

                    Transform decorationTransform = gridTransform.Find(decorationChildName);
                    if (decorationTransform != null)
                    {
                        TilemapRenderer decorationRenderer = decorationTransform.GetComponent<TilemapRenderer>();
                        if (decorationRenderer != null)
                        {
                            decorationRenderer.enabled = false;
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"no esta el child '{decorationChildName}' en '{gridChildName}'.");
                    }
                }
                else
                {
                    Debug.LogWarning($"No esta el child '{gridChildName}' en '{mapObjectName}'.");
                }
            }
            else
            {
                Debug.LogWarning($" '{mapObjectName}' no fue encontrado.");
            }

            Destroy(gameObject);
        }
    }
}
