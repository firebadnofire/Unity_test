using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;  // Set the initial health value
    private PlayerDeathHandler playerDeathHandler;
    private bool isImmune = false; // Flag to track immunity status

    private void Start()
    {
        playerDeathHandler = GetComponent<PlayerDeathHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the 'k' key is pressed to toggle immunity
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleImmunity();
        }
    }

    // Method to toggle player immunity
    private void ToggleImmunity()
    {
        isImmune = !isImmune;

        if (isImmune)
        {
            Debug.Log("Player is now immune to damage.");
        }
        else
        {
            Debug.Log("Player is no longer immune to damage.");
        }
    }

    // Method to deal damage to the player
    public void TakeDamage(int damageAmount)
    {
        // Check if the player is immune to damage
        if (isImmune)
        {
            Debug.Log("Player is immune to damage.");
            return; // Do not apply damage if immune
        }

        health -= damageAmount;  // Subtract the damage amount from the player's health
        if (health < 0)          // Clamp the health at zero
        {
            health = 0;
        }

        CheckDeath();  // Check if the player is dead after taking damage
    }

    // Method to check if the player is dead
    private void CheckDeath()
    {
        if (health <= 0)
        {
            if (playerDeathHandler != null)
            {
                playerDeathHandler.HandleDeath();  // Handle death using the PlayerDeathHandler script
            }
            else
            {
                Debug.LogError("PlayerDeathHandler script not found!");
            }
        }
    }

    private void PrintHealth()
    {
        Debug.Log("Player Health: " + health);
    }
}
