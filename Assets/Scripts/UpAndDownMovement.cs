using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDownMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float maxHeight = 2f;
    public float minHeight = 0f;

    private bool movingUp = true;

    void Update()
    {
        if (movingUp)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

            if (transform.position.y >= maxHeight)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

            if (transform.position.y <= minHeight)
            {
                movingUp = true;
            }
        }
    }
}
