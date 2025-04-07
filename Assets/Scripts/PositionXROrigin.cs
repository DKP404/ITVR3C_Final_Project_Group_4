
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PositionXROrigin : MonoBehaviour
{
    public Vector3 desiredOriginPosition;

    void Start()
    {
        StartCoroutine(AlignXROrigin());
    }

    System.Collections.IEnumerator AlignXROrigin()
    {
        yield return null; // wait one frame for XR to initialize

        XROrigin xrOrigin = GetComponent<XROrigin>();
        if (xrOrigin != null)
        {
            Transform cameraTransform = xrOrigin.Camera.transform;
            Vector3 cameraOffset = cameraTransform.position - xrOrigin.transform.position;

            // Calculate how much to move the XR Origin so the camera ends up at the desired spot
            Vector3 offsetToApply = desiredOriginPosition - cameraOffset;
            xrOrigin.transform.position = offsetToApply;
        }
    }
}