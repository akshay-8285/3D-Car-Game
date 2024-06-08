using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudio : MonoBehaviour
{
   
    public AudioClip carSound; // Car ke regular sound
    public AudioClip accidentSound; // Car ke accident sound

    private AudioSource audioSource; // Audio source component

    void Start()
    {
      
        audioSource = GetComponent<AudioSource>(); // Car ke AudioSource component ko retrieve karein

        if (carSound != null && audioSource != null)
        {
            // Car ke regular sound ko play karein
            audioSource.clip = carSound;
            audioSource.loop = true; // Loop karne ke liye set karein
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Car sound or AudioSource not assigned!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check karein ki collision obstacle se hui hai ya nahi
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Check karein ki accident sound assign hai ya nahi
            if (accidentSound != null && audioSource != null)
            {
                // Car ke accident sound ko play karein
                audioSource.Stop(); // Pehle kiya gaya sound stop karein
                audioSource.clip = accidentSound;
                audioSource.loop = false; // Ek baar hi play karein
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Accident sound or AudioSource not assigned!");
            }

            // Yahan aur bhi logic add kar sakte hain, jaise ki health ya score kam karna
        }
    }
}
