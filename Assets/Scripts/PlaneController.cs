using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlaneControllerXR : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxSpeed = 50f;
    public float liftOffSpeed = 20f;
    public float liftForce = 5f;
    public Transform cameraTransform;

    private float currentSpeed = 0f;
    private Vector3 cameraOffset;
    private InputDevice leftHand;

    void Start()
    {
        if (cameraTransform != null)
            cameraOffset = cameraTransform.position - transform.position;

        // Get the left-hand XR controller
        var leftHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        if (leftHandDevices.Count > 0)
            leftHand = leftHandDevices[0];
    }

    void Update()
    {
        if (!leftHand.isValid) return;

        // Get thumbstick input (Y axis)
        if (leftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 joystickInput))
        {
            float input = joystickInput.y;

            // Adjust speed
            currentSpeed += input * acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

            // Move forward
            transform.position += transform.forward * currentSpeed * Time.deltaTime;

            // Lift off
            if (currentSpeed >= liftOffSpeed)
                transform.position += transform.up * liftForce * Time.deltaTime;

            // Keep camera offset
            if (cameraTransform != null)
                cameraTransform.position = transform.position + cameraOffset;
        }
    }
}