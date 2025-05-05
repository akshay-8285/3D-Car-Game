using UnityEngine;

public class CarAudio : MonoBehaviour
{
    public static CarAudio Instance;
    
    [Header("Engine Sound")]
    public float minSpeed = 5f;
    public float maxSpeed = 100f;
    public float minPitch = 0.7f;
    public float maxPitch = 2.5f;
    public AudioSource engineAudioSource;
    
    [Header("Brake Sound")]
    public AudioClip brakeClip;
    [Range(0,1)] public float brakeVolume = 0.5f;
    public float brakeSoundThreshold = 5f; // Minimum speed to play brake sound

    private Rigidbody carRigidbody;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        if(!engineAudioSource) engineAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(carRigidbody != null)
            UpdateEngineSound();
    }

    void UpdateEngineSound()
    {
        float currentSpeed = carRigidbody.linearVelocity.magnitude;
        float speedRatio = Mathf.Clamp01(currentSpeed / maxSpeed);
        
        engineAudioSource.pitch = Mathf.Lerp(minPitch, maxPitch, speedRatio);
        engineAudioSource.volume = 0.5f + speedRatio * 0.5f; // Volume increases with speed
    }

    public void PlayBrakeSound()
    {
        if(brakeClip != null && carRigidbody.linearVelocity.magnitude > brakeSoundThreshold)
        {
            AudioSource.PlayClipAtPoint(brakeClip, transform.position, brakeVolume);
        }
    }
}