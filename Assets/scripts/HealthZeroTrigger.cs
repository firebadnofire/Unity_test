using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthZeroTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth component
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Set the player's health to 0
                playerHealth.TakeDamage(playerHealth.health);
            }
            else
            {
                Debug.LogError("PlayerHealth script not found on the player!");
            }
        }
    }
}
