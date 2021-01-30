using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampCamera : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    public float offsetDistance = -4f;
    public float offsetHeight = 3f;

    void Update()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, offsetHeight, offsetDistance));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
