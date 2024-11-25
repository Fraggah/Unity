using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] private AudioClip keyGained;
    private AudioSource audiosource;

    private void OnEnable()
    {
        audiosource = GetComponent<AudioSource>();
        if (audiosource == null)
        {
            Debug.LogError("falta audiosource");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();

            if (playerScript != null)
            {
                Debug.Log("encontraste la llave!");
                if (!audiosource.isPlaying)
                {
                    audiosource.PlayOneShot(keyGained);
                }
                Destroy(gameObject, keyGained.length);
            }
        }
    }
}
