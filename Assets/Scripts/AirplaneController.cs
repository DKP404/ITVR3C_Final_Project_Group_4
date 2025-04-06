using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Flight Settings")]
    public float thrustPower = 100f;  // Power of the engines
    public float maxSpeed = 50f;  // Max speed of the airplane
    public float liftForce = 5f;  // Lift force to make the airplane take off
    public float turnSpeed = 20f;  // Rotation speed (Yaw, Pitch, Roll)
    public float brakeForce = 50f;  // Brake power
    public float dragGround = 1f;  // Drag when on ground
    public float dragAir = 0.05f;  // Drag when flying

    private float thrust = 0f;
    private bool isOnGround = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = dragGround; // Set initial drag (on ground)
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        ApplyForces();
        CheckGroundStatus();
    }

    void HandleInput()
    {
        // Throttle Control (W = Increase, S = Decrease)
        if (Input.GetKey(KeyCode.W))
            thrust += thrustPower * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            thrust -= thrustPower * Time.deltaTime;

        thrust = Mathf.Clamp(thrust, 0, maxSpeed); // Limit thrust

        // Steering (A = Left, D = Right)
        if (Input.GetKey(KeyCode.A))
            rb.AddTorque(transform.up * -turnSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            rb.AddTorque(transform.up * turnSpeed * Time.deltaTime);

        // Pitch (Up/Down) for Climbing or Descending
        if (Input.GetKey(KeyCode.UpArrow))
            rb.AddTorque(transform.right * -turnSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            rb.AddTorque(transform.right * turnSpeed * Time.deltaTime);

        // Roll (Left/Right Tilt)
        if (Input.GetKey(KeyCode.LeftArrow))
            rb.AddTorque(transform.forward * turnSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            rb.AddTorque(transform.forward * -turnSpeed * Time.deltaTime);

        // Braking (SPACE for Ground Brake)
        if (Input.GetKey(KeyCode.Space) && isOnGround)
            rb.velocity *= 0.95f; // Reduce speed when braking on the ground
    }

    void ApplyForces()
    {
        // Apply Forward Thrust
        rb.AddForce(transform.forward * thrust, ForceMode.Force);

        // Apply Lift when moving fast enough
        if (!isOnGround && rb.velocity.magnitude > 10f)
            rb.AddForce(Vector3.up * liftForce * thrust * 0.02f, ForceMode.Force);
    }

    void CheckGroundStatus()
    {
        // Raycast to check if airplane is on the ground
        isOnGround = Physics.Raycast(transform.position, -transform.up, 1.5f);
        rb.drag = isOnGround ? dragGround : dragAir; // Adjust drag
    }
}
