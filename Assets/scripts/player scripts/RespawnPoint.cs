using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public static Vector3 respawnPosition; // Static variable to store the respawn position

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            respawnPosition = transform.position; // Store the respawn position when the player enters the trigger
        }
    }
}
