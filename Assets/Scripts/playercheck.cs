using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercheck : MonoBehaviour
{
    public Text collectionText; // Reference to the Text component for displaying collection info
    private bool playerAlive = true; // Flag to track if the player is alive

    private static playercheck instance;
    public AudioClip collectedMaxSound;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Find all objects with the "grabbable.cs" script and calculate the maximum count
        grabbable[] grabbableObjects = FindObjectsOfType<grabbable>();
        int maxObjects = grabbableObjects.Length;

        // Initialize the UI text with the initial values
        UpdateUI(0, maxObjects); // Use the type name to call the static method
    }

    public static void UpdateUI(int collected, int max)
    {
        instance.collectionText.text = "Collected: " + collected + " of " + max + " objects";

        // Check if "collected" equals "max" and update the text accordingly
        if (collected == max && instance.playerAlive)
        {
            instance.collectionText.text = "All objects collected!";
            PlayCollectedMaxSound();
        }
    }

    private static void PlayCollectedMaxSound()
    {
        if (instance.collectedMaxSound != null)
        {
            // Create an AudioSource and play the collectedMaxSound
            AudioSource audioSource = instance.gameObject.AddComponent<AudioSource>();
            audioSource.clip = instance.collectedMaxSound;
            audioSource.Play();

            // Destroy the AudioSource component after the sound has finished playing
            Destroy(audioSource, instance.collectedMaxSound.length);
        }
    }

    // Function to set the player alive status
    public static void SetPlayerAlive(bool alive)
    {
        instance.playerAlive = alive;
    }
}
