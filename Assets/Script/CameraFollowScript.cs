using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    //public Transform target; // Player car ka reference

    //public Vector3 offset; // Camera ka position offset
    //public float smoothSpeed = 0.125f; // Camera ka smooth transition speed

    //void LateUpdate()
    //{
    //    if (target != null)
    //    {
    //        Vector3 desiredPosition = target.position + offset; // Player car ke position par camera ko set karein
    //        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Smoothly transition karein
    //        transform.position = smoothedPosition; // Update the camera position
    //    }
    //}

    public Transform car; // Car GameObject ka reference
    public float cameraDistance = 2f; // Camera ka distance car se
    public float cameraHeight = 1f; // Camera ki height car se
    public float cameraFollowSpeed = 5f; // Camera ka follow speed

    void LateUpdate()
    {
        if (car != null)
        {
            // Car ke position ko follow karna
            Vector3 targetPosition = car.position - car.forward * cameraDistance + Vector3.up * cameraHeight;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraFollowSpeed);

            // Car ke direction mein camera ko rotate karna
            transform.LookAt(car);
        }
    }
}
