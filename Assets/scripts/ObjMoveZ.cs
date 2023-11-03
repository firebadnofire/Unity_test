using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoveZ : MonoBehaviour
{
    public float moveDistance = 2.0f;  // Adjust this to set the distance of the movement.
    public float moveSpeed = 2.0f;     // Adjust this to set the speed of the movement.

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool movingForward = true;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + new Vector3(0, 0, moveDistance);
    }

    void Update()
    {
        float step = moveSpeed * Time.deltaTime;

        if (movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
            if (transform.position == endPosition)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, step);
            if (transform.position == startPosition)
            {
                movingForward = true;
            }
        }
    }
}
