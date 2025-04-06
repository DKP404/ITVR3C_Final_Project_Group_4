using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera[] cameras; // Assign cameras in the Inspector
    private int currentCameraIndex = 0;

    void Start()
    {
        // Ensure only the first camera is enabled
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchToNextCamera();
        }
    }

    void SwitchToNextCamera()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }

}
