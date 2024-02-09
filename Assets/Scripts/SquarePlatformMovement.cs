using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePlatformMovement : MonoBehaviour
{
    public float size = 5f; // Size of the square
    public float speed = 2f; // Speed of movement
    private Vector3 startPosition;
    private float timeCounter = 0f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate movement along the square path
        float x = Mathf.Cos(timeCounter * speed) * size;
        float z = Mathf.Sin(timeCounter * speed) * size;

        // Update the position of the platform
        transform.position = startPosition + new Vector3(x, 0f, z);

        // Increment time counter for the next frame
        timeCounter += Time.deltaTime;
    }
}
