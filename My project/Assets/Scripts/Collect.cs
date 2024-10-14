using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    [SerializeField] private List<GameObject> keys; 
    [SerializeField] private GameObject bag;
    [SerializeField] private AudioClip keySound;
    private AudioSource audioSource; 

    private void Awake()
    {
        keys = new List<GameObject>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Collectable")) { return; }

        GameObject newKey = collision.gameObject;
        newKey.SetActive(false);

        keys.Add(newKey);
        newKey.transform.SetParent(bag.transform);

        if (audioSource != null && keySound != null)
        {
            audioSource.PlayOneShot(keySound);
        }
    }

    public List<GameObject> GetKeys()
    {
        return keys;
    }

}