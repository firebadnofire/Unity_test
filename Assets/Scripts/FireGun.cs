using UnityEngine;

public class FireGun : MonoBehaviour
{
    public int maxShots = 30;
    private int currentShots;

    private void Start()
    {
        currentShots = maxShots;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button (mouse 1) clicked
        {
            if (currentShots > 0)
            {
                ShootRay();
                currentShots--;
                Debug.Log("Shots left: " + currentShots);
            }
            else
            {
                Debug.Log("Out of ammo! Press 'R' to reload.");
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) // Press 'R' to reload
        {
            currentShots = maxShots;
            Debug.Log("Reloaded. Shots available: " + currentShots);
        }
    }

    private void ShootRay()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, mainCamera.transform.forward, out hit))
            {
                // Do something with the hit information (e.g., apply damage, interact with objects)
                // For now, let's just log what we hit.
                Debug.Log("Ray hit: " + hit.collider.gameObject.name);
            }
        }
    }
}
