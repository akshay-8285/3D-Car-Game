using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public static CarAudio Instance;
    public float minSpeed;
    public float maxSpeed;
    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;
    public AudioSource engineAudioSource;
    public AudioClip brakeClip;
    // public AudioClip driveClip;
    private Rigidbody carRigidbody;
    private float currentSpeed;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
       
    }

    public void Start()
    {
        engineAudioSource = GetComponent<AudioSource>();
        carRigidbody = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        EngineSound();
    }
    
    public void EngineSound()
    {
        currentSpeed = carRigidbody.linearVelocity.magnitude;
        pitchFromCar = carRigidbody.linearVelocity.magnitude / 50f;

        if(currentSpeed < minSpeed)
        {
            engineAudioSource.pitch = minPitch;
        }
        if (currentSpeed > maxSpeed && currentSpeed < maxSpeed)
        {
            engineAudioSource.pitch = minPitch + pitchFromCar;
        }
        if (currentSpeed > maxSpeed)
        {
            engineAudioSource.pitch = maxPitch;
        }

       
    }

    public void BrakeSound()
    {
        if (engineAudioSource != null)
        {
            engineAudioSource.PlayOneShot(brakeClip);
        }
    }
   
}
