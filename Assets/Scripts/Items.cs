using UnityEngine;

public class Items : MonoBehaviour
{
    public float floatHeight = 1.0f;  // Adjust this to set the floating height
    public float rotationSpeed = 30.0f;  // Adjust this to set the rotation speed

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Disable gravity for the object
    }

    private void Update()
    {
        // Make the object float at the desired height
        Vector3 floatPosition = new Vector3(transform.position.x, floatHeight, transform.position.z);
        transform.position = floatPosition;

        // Rotate the object around its up axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
