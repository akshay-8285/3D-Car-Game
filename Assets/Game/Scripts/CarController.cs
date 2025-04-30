using System.Collections.Generic;
using UnityEngine;
using System;

public class CarController : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject tireEffect;
        public ParticleSystem smokeEffect;
        public Axel axel;
    }

    public float maxAcceleration = 30.0f;
    public float breakAcceleration = 50.0f;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 50.0f;
    public List<Wheel> wheels = new List<Wheel>();
    float moveInput;
    float turnInput;

    private Rigidbody carRb;
    private Vector3 centerOfMass;
    private float totalRotation;
    private float lastYRotation;
    private bool isDoing360 = false;

    [SerializeField] private GameObject leftBreakLight , rightBreakLight;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = centerOfMass;
        leftBreakLight.SetActive(false);
        rightBreakLight.SetActive(false);
    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
        EffectWheel();
        Rotate360();
       
    }

    public void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    public void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput  * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    public void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = turnInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle,  0.6f);
            }
        }
    }

    public void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = breakAcceleration   * 300 * Time.deltaTime;
                leftBreakLight.SetActive(true);
                rightBreakLight.SetActive(true);
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
                leftBreakLight.SetActive(false);
                rightBreakLight.SetActive(false);
            }
        }
    }

    public void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
            wheel.wheelModel.transform.position = position;
            wheel.wheelModel.transform.rotation = rotation;
        }
    }

    public void Rotate360()
    {
        
        float currentYRotation = transform.eulerAngles.y;
        float deltaRotation = Mathf.DeltaAngle(lastYRotation, currentYRotation);
        totalRotation += Mathf.Abs(deltaRotation);

        if(!Input.GetKey(KeyCode.Space) || carRb.linearVelocity.magnitude < 2f)
        {
            isDoing360 = false;
            totalRotation = 0f;
        }
        if (totalRotation >= 360f && !isDoing360)
        {
            isDoing360 = true;
            Debug.Log("360 Done!");
            CarAudio.Instance.BrakeSound();
        }


        lastYRotation = currentYRotation;

        Debug.Log("Total Rotation : " + totalRotation);
    }

    public void EffectWheel()
    {
        foreach (var wheel in wheels)
        {
            if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear)
            {
                wheel.tireEffect.GetComponentInChildren<TrailRenderer>().emitting = true;
                wheel.smokeEffect.Emit(1);
            }
            else
            {
                wheel.tireEffect.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
        }
    }
}
