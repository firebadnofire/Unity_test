using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    public float health = 100f;  // The health of the object

    // Method to call when the object is shot
    public void OnShot(float damage)
    {
        TakeDamage(damage);
    }

    // Apply damage to the object
    void TakeDamage(float damage)
    {
        health -= damage;  // Subtract the damage from the health

        // If health drops to zero or below, destroy the object
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}

