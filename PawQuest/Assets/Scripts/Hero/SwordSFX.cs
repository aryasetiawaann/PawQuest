using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSFX : MonoBehaviour
{
    public AudioSource swingSound, hitSound;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the sword hits an enemy
        if (other.CompareTag("Enemy"))
        {
            // Play hit sound if it hits an enemy
            hitSound.Play();
        }
    }

    private void Update()
    {
        // Attack sound logic
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            // Play swing sound when attacking
            swingSound.Play();
        }
    }
}