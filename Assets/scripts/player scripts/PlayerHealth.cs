using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;  // Set the initial health value
    private PlayerDeathHandler playerDeathHandler;

    private void Start()
    {
        playerDeathHandler = GetComponent<PlayerDeathHandler>();
    }

    // Method to deal damage to the player
    public void TakeDamage(int damageAmount)
    {
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
}
