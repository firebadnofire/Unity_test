using UnityEngine;

public class ObjMove : MonoBehaviour
{
    public float moveDistance = 5f;     // The distance the object should move.
    public float moveSpeed = 2f;       // The speed at which it moves.

    private Vector3 initialPosition;    // The initial position of the object.
    private float direction = 1f;       // The direction of movement (+1 for right, -1 for left).

    void Start()
    {
        initialPosition = transform.position; // Store the initial position of the object.
    }

    void Update()
    {
        // Calculate the new position based on the direction and speed.
        Vector3 newPosition = transform.position + new Vector3(direction * moveSpeed * Time.deltaTime, 0f, 0f);

        // Check if the object has reached the maximum move distance in either direction.
        if (Vector3.Distance(initialPosition, newPosition) >= moveDistance)
        {
            // Change the direction to reverse the movement.
            direction *= -1f;
        }

        // Update the object's position.
        transform.position = newPosition;
    }
}
