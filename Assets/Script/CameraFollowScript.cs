using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private Transform car;
    public Vector3 moveOffset;
    public Vector3 rotationOffset;
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;

    public void FixedUpdate()
    {
      HandleMovement();
      HandleRotation();

    }

    public void HandleMovement()
    {
        Vector3 targetPos = new Vector3();
        targetPos = car.TransformPoint(moveOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * movementSpeed);
    }
    public void HandleRotation()
    {
        var direction  = car.position - transform.position;
        var rotation = new Quaternion();
        rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}
