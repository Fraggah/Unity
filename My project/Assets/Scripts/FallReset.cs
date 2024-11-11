using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FallReset : MonoBehaviour
{
    [SerializeField] private PlayableDirector timelineDirector;
    [SerializeField] private float damage = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player != null && player.PlayerProfile != null)
            {
                player.PlayerProfile.Lifes -= (int)damage;
                Debug.Log("Vidas restantes: " + player.PlayerProfile.Lifes);

                collision.transform.position = GameManager.instance.initialPlayerPosition;

                Camera.main.transform.position = GameManager.instance.cameraPositions[0];

                if (timelineDirector != null)
                {
                    timelineDirector.Play();
                }
            }
        }
    }
}
