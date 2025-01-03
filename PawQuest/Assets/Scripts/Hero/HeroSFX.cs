using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSFX : MonoBehaviour
{
    public AudioSource footstepsSound, sprintSound, attackSound;

    void Update()
    {
        // Movement sound logic
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                footstepsSound.enabled = false;
                sprintSound.enabled = true;
            }
            else
            {
                footstepsSound.enabled = true;
                sprintSound.enabled = false;
            }
        }
        else
        {
            footstepsSound.enabled = false;
            sprintSound.enabled = false;
        }

        // Attack sound logic
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            attackSound.Play();
        }
    }
}