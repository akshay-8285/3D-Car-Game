using System.Collections.Generic;
using UnityEngine;
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
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
            

    }

    void Start()
    {
        changeButton.onClick.AddListener(ChangeMusic);

        if(sounds.Length > 0)
        {
            PlaySound(sounds[0]);
        }
    }

   

    public void ChangeMusic()
    {
        if(sounds.Length > 0)
        {
            int randomIndex = Random.Range(0, sounds.Length);
            PlaySound(sounds[randomIndex]);
        }
    }
    public void PlaySound(Sound sound)
    {
        audioSource.Stop();
        audioSource.clip = sound.clip;
        audioSource.loop = sound.loop;
        audioSource.volume = sound.volume;
        audioSource.pitch = sound.pitch;
        audioSource.Play();
    }
}
