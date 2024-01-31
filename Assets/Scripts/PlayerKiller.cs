using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger zone is the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth component from the player GameObject
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // Set the player's health to 0
            if (playerHealth != null)
            {
                playerHealth.currentHealth = 0;
                playerHealth.Die(); // Call the Die() method if you have it to handle the player's death
            }
        }
    }
}