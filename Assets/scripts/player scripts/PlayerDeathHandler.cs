using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathHandler : MonoBehaviour
{
    public AudioClip deathClip;  // Drag your death audio clip here in the inspector
    public Image redOverlay;  // Drag your red overlay image here in the inspector
    public Text respawnText;  // Reference to the UI Text element
    private AudioSource audioSource;
    private Movement movement;
    private CharacterController characterController;
    private int currentHP = 100;  // Replace with your actual health system or variable
    private bool isRespawnAvailable = false; // Track if respawn is available

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
        characterController = GetComponent<CharacterController>();

        if (redOverlay != null)
        {
            redOverlay.gameObject.SetActive(false);  // Disable the red overlay until needed
        }

        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(false);  // Disable the respawn text initially
        }
    }

    private void Update()
    {
        // Check for respawn input only when HP is at or below 0
        if (currentHP <= 0)
        {
            isRespawnAvailable = true; // Respawn is available
            if (respawnText != null)
            {
                respawnText.gameObject.SetActive(true); // Show the respawn text
            }
        }

        // Check for respawn key input and respawn availability
        if (isRespawnAvailable && Input.GetKeyDown(KeyCode.G))
        {
            Respawn();
        }
    }

    public void HandleDeath()
    {
        // Reduce HP to or below 0 to trigger respawn
        currentHP = 0;

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

    private void Respawn()
    {
        // Reset the player's position to the stored respawn position
        if (movement != null)
        {
            movement.enabled = true; // Enable player controls
            characterController.enabled = false; // Disable character controller briefly to change position
            characterController.transform.position = RespawnPoint.respawnPosition; // Set player's position
            characterController.enabled = true; // Re-enable character controller
        }

        // Restore HP to a starting value or other respawn logic
        currentHP = 100;  // Replace with your actual starting HP value

        // Additional respawn logic (e.g., reset health, animations, etc.) can be added here

        // Disable the red overlay
        if (redOverlay != null)
        {
            redOverlay.gameObject.SetActive(false);
        }

        // Disable the respawn text
        if (respawnText != null)
        {
            respawnText.gameObject.SetActive(false);
        }

        isRespawnAvailable = false; // Respawn is no longer available after respawn
    }
}
