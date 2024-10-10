using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; 

public class FallReset : MonoBehaviour
{
    [SerializeField] private PlayableDirector timelineDirector; 
    [SerializeField] float damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(-damage);
            Debug.Log("Vidas: " + player.lifes);

            collision.transform.position = GameManager.instance.initialPlayerPosition;

            Camera.main.transform.position = GameManager.instance.cameraPositions[0];

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
