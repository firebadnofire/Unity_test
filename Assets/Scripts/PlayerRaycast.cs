using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public float raycastDistance = 20f; // How far the ray should reach
    public int damageAmount = 10; // Amount of damage to subtract from HP

    void Update()
    {
        // Check for mouse click (Mouse Button 1)
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray forward from the player's position and direction
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                // Check if the hit object has the "HealthSystem" script
                HealthSystem healthSystem = hit.collider.GetComponent<HealthSystem>();

                if (healthSystem != null)
                {
                    // Subtract damageAmount from the object's HP
                    healthSystem.TakeDamage(damageAmount);
                }
            }
        }
    }
}
