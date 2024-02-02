using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabbable : MonoBehaviour
{
    private static int maxObjects;
    public AudioSource collectedSound;
    private static int collectedObjects = 0;
    private static Vector3 lastCollectedObjectPosition; // Store the position of the last collected object

    private void Start()
    {
        // Count how many objects in the scene have the "grabbable.cs" script attached
        maxObjects = GameObject.FindObjectsOfType<grabbable>().Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player touched this object
            if (collectedObjects < maxObjects)
            {
                collectedObjects++;
                // Store the position of the last collected object
                lastCollectedObjectPosition = transform.position;
                
                // Play the collected sound if it's assigned to the collectedSound AudioSource.
                if (collectedSound != null)
                {
                    collectedSound.Play();
                }
                gameObject.SetActive(false); // Make the object disappear
                playercheck.UpdateUI(collectedObjects, maxObjects); // Update UI
                
                // Print the XYZ coordinates to the console
                Debug.Log("Last Collected Object Position (XYZ): " + lastCollectedObjectPosition);
            }
        }
    }

    // You can access the last collected object's position using this function
    public static Vector3 GetLastCollectedObjectPosition()
    {
        return lastCollectedObjectPosition;
    }
}
