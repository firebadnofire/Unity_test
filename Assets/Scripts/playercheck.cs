using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercheck : MonoBehaviour
{
    public Text collectionText; // Reference to the Text component for displaying collection info

    private static playercheck instance;

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
        playercheck.UpdateUI(0, maxObjects); // Use the type name to call the static method
    }

    public static void UpdateUI(int collected, int max)
    {
        instance.collectionText.text = "Collected: " + collected + " of " + max + " objects";
    }
}
