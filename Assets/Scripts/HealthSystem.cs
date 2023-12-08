using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHP = 100; // Maximum HP of the object
    private int currentHP; // Current HP of the object
    private bool isRagdoll = false;
    public GameObject ragdollPrefab; // Ragdoll prefab to replace the object when it dies

    private void Start()
    {
        currentHP = maxHP; // Initialize current HP to max HP
    }

    public void TakeDamage(int damage)
    {
        if (!isRagdoll)
        {
            currentHP -= damage; // Decrease HP by the damage amount

            Debug.Log("Current HP: " + currentHP); // Print current HP to the console

            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        // Spawn the ragdoll prefab at the same position and rotation as the current object
        GameObject ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        Destroy(gameObject); // Destroy the current object

        isRagdoll = true;
    }
}
