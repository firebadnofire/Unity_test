using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHP = 100; // Set the maximum HP for the object.
    private int currentHP; // Current HP of the object.
    public AudioClip destructionSound; // Sound to play when the object is destroyed.

    private void Start()
    {
        currentHP = maxHP; // Initialize current HP to max HP.
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHP > 0)
        {
            currentHP -= damageAmount; // Reduce HP by the damage amount.

            if (currentHP <= 0)
            {
                currentHP = 0;
                DestroyObject();
            }

            // Output the current HP to the console.
            Debug.Log("Object HP: " + currentHP);
        }
    }

    private void DestroyObject()
    {
        // Play the destruction sound if it's set.
        if (destructionSound != null)
        {
            AudioSource.PlayClipAtPoint(destructionSound, transform.position);
        }

        // Hide or destroy the object. You can choose either option.
        //gameObject.SetActive(false); // Hide the object.
        Destroy(gameObject); // Destroy the object.
    }
}
