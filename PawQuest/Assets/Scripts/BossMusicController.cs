using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicController : MonoBehaviour
{
    public AudioSource bgMusic; 
    public AudioClip bossMusic; 
    private AudioClip originalMusic;

    void Start()
    {
        // Store the original background music
        originalMusic = bgMusic.clip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            bgMusic.clip = bossMusic; 
            bgMusic.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bgMusic.clip = originalMusic; 
            bgMusic.Play();
        }
    }
}