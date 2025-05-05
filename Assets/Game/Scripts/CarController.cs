using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

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
    private bool isAccelerating = false;
    private bool isBraking = false;
    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    [SerializeField] private GameObject leftBreakLight , rightBreakLight;

    void Start()
    {

        ControllEvent();
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
        Debug.Log("Move Input : " + moveInput);
        if (isAccelerating)
        {
            moveInput = 1f;
        }
        if (isBraking)
        {
            moveInput = -1f;
        }
        if (isTurningLeft)
        {
            turnInput = -1f;
        }
        if (isTurningRight)
        {
            turnInput = 1f;
        }
    }

    public void Move()
    {
            
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput  * 600 * maxAcceleration * Time.deltaTime;
            
            if(!isBraking)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
            // else
            // {
            //     wheel.wheelCollider.brakeTorque = breakAcceleration * 300 * Time.deltaTime;
            // }
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
                CarAudio.Instance.PlayBrakeSound();
            }
        }

        else if (isBraking)
        {
            leftBreakLight.SetActive(true);
            rightBreakLight.SetActive(true);
        }
        else
        {
            leftBreakLight.SetActive(false);
            rightBreakLight.SetActive(false);
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
           
        }


        lastYRotation = currentYRotation;

        Debug.Log("Total Rotation : " + totalRotation);
    }

    public void EffectWheel()
    {
        foreach (var wheel in wheels)
        {
            if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear || isBraking && wheel.axel == Axel.Rear)
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

    public void AccelerateButtonDown()
    {
        isAccelerating = true;
    }
    public void AccelerateButtonUp()
    {
        isAccelerating = false;
    }
    public void BrakeButtonDown()
    {
        isBraking = true;
    }
    public void BrakeButtonUp()
    {
        isBraking = false;

    }
    public void LeftButtonDown()
    {
        isTurningLeft = true;
    }
    public void LeftButtonUp()
    {
        isTurningLeft = false;
    }
    public void RightButtonDown()
    {
        isTurningRight = true;
    }
    public void RightButtonUp()
    {
        isTurningRight = false;
    }

    public void ControllEvent()
    {
        CarInputEvent.OnAccleratePressed += AccelerateButtonDown;
        CarInputEvent.OnAcclerateReleased += AccelerateButtonUp;
        CarInputEvent.OnBrakePressed += BrakeButtonDown;
        CarInputEvent.OnBrakeReleased += BrakeButtonUp;
        CarInputEvent.OnLeftPressed += LeftButtonDown;
        CarInputEvent.OnLeftReleased += LeftButtonUp;
        CarInputEvent.OnRightPressed += RightButtonDown;
        CarInputEvent.OnRightReleased += RightButtonUp;
    }
}
