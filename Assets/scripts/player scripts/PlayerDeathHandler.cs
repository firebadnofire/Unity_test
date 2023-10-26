using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathHandler : MonoBehaviour
{
    public AudioClip deathClip;  // Drag your death audio clip here in the inspector
    public Image redOverlay;  // Drag your red overlay image here in the inspector
    private AudioSource audioSource;
    private Movement movement;  // Updated this line to use your Movement script

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();  // Updated this line to use your Movement script

        if (redOverlay != null)
        {
            redOverlay.gameObject.SetActive(false);  // Disable the red overlay until needed
        }
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

        // Enable the red overlay
        if (redOverlay != null)
        {
            redOverlay.gameObject.SetActive(true);
        }

        // You can add any other death-related logic here
    }
}
