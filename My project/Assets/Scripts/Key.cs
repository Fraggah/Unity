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
            Debug.LogError("AudioSource no encontrado");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerScript = collision.GetComponent<Player>();

            if (playerScript != null)
            {
                Debug.Log("Encontraste una llave!");
                if (!audiosource.isPlaying)
                {
                    audiosource.PlayOneShot(keyGained);
                }
                // Destruye el objeto 
                Destroy(gameObject, keyGained.length);
            }
        }
    }
}
