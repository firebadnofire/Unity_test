using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public AudioSource audioSource;
    public AudioClip deathSound; // Assign your death sound clip in the Inspector

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the player's health has dropped to zero or below
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Play the death sound when the player dies
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        // Implement what happens when the player dies (e.g., game over or respawn)
        // You can add more logic here based on your game's requirements
        Debug.Log("Player has died!");
    }
}
