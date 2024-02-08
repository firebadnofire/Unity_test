using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioSource audioSource;
    public AudioClip deathSound; // Assign your death sound clip in the Inspector
    public Text healthText;
    public Text deathText;

    private bool hasDied = false; // Flag to track if the player has already died
    private Controller playerController; // Reference to the Controller script

    void Start()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<Controller>(); // Assuming your Controller script is attached to the same GameObject as PlayerHealth
        UpdateHealthUI();
    }

    void Update()
    {
        if (hasDied && Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!hasDied) // Check if the player hasn't already died
        {
            currentHealth -= damage;

            // Check if the player's health has dropped to zero or below
            if (currentHealth <= 0)
            {
                Die();
            }

            UpdateHealthUI();
        }
    }

    public void Die()
    {
        if (!hasDied) // Check if the player hasn't already died
        {
            hasDied = true; // Set the flag to indicate that the player has died

            // Play the death sound when the player dies
            if (audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound);
            }
            // Implement what happens when the player dies (e.g., game over or respawn)
            // You can add more logic here based on your game's requirements
            Debug.Log("Player has died!");

            // Disable the Controller script to take away player control
            if (playerController != null)
            {
                playerController.enabled = false;
            }

            // Notify playercheck script that the player is dead
            playercheck.SetPlayerAlive(false);

            deathText.text = "Press R to respawn";
        }
    }

    private void RespawnPlayer()
    {
        // Check if there is a last collected object position
        if (grabbable.GetLastCollectedObjectPosition() != Vector3.zero)
        {
            // Teleport the player to the last collected object's position
            transform.position = grabbable.GetLastCollectedObjectPosition();
        }
        else
        {
            // Teleport the player to (0, 0, 0) if no last collected object position is found
            transform.position = Vector3.zero;
        }

        // Restore player controls
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Reset the hasDied flag
        hasDied = false;

        // Notify playercheck script that the player has respawned
        playercheck.SetPlayerAlive(true);

        deathText.text = "";
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }
}
