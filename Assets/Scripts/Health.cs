using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform any actions when the object's HP reaches 0 (e.g., play death animation, spawn particles, etc.)
        // For now, let's just deactivate the GameObject.
        gameObject.SetActive(false);
    }
}
