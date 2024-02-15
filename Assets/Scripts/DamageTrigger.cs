using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public int damageAmount = 50; // Editable damage amount in the Unity editor

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth component of the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            
            // Check if the PlayerHealth component is not null
            if (playerHealth != null)
            {
                // Call the TakeDamage method of the player's health script with the damageAmount
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
