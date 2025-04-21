using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sounds : MonoBehaviour
{
    public Button changeButton;
    [System.Serializable]
    public struct Sound
    {
        public AudioClip clip;
        public bool loop;
        public float volume;
        public float pitch;
    }

    public Sound[] sounds;

    void Awake()
    {
         InitializeSounds();
    }
    public void Start()
    {
       
        changeButton.onClick.AddListener(ChangeMusic);

    }


    public void InitializeSounds()
    {
        foreach (Sound sound_ in sounds)
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = sound_.clip;
            audioSource.loop = sound_.loop;
            audioSource.volume = sound_.volume;
            audioSource.pitch = sound_.pitch;
            // audioSource.PlayOneShot(sound_.clip, sound_.volume);
        }
    }
    public void ChangeMusic()
    {
        if (sounds.Length > 0)
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = sounds[Random.Range(0, sounds.Length)].clip;
            audioSource.PlayOneShot(audioSource.clip, 1f);
        }
    }
    
}
