using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    public AudioClip deathClip;  // Drag your death audio clip here in the inspector
    private AudioSource audioSource;
    private Movement movement;  // Updated this line to use your Movement script

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();  // Updated this line to use your Movement script
    }

    public void HandleDeath()
    {
        // Disable player controls
        if (movement != null)
        {
            movement.enabled = false;
        }

        // Play death audio clip
        if (deathClip != null)
        {
            audioSource.clip = deathClip;
            audioSource.Play();
        }

        // You can add any other death-related logic here
    }
}

