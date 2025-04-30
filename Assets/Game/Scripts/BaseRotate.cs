using UnityEngine;

public class BaseRotate : MonoBehaviour
{
    public float rotateSpeed;

    
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
