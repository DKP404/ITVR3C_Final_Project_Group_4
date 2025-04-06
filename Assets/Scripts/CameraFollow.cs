using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Assign the object to follow (e.g., your airplane)
    public Vector3 offset = new Vector3(0, 5, -10); // Camera position offset
    public float smoothSpeed = 5f; // Speed of following

    void LateUpdate()
    {
        if (target == null) return;

        // Desired camera position
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Smoothly move the camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Make the camera look at the target
        transform.LookAt(target);
    }
}
