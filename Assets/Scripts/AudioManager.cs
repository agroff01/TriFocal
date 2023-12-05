using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioClip defaultAudioClip;
    
    private AudioSource audioSource;

    void Awake()
    {
        // Ensure there's only one instance of the AudioManager
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // Add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Start playing the default audio 
        if (defaultAudioClip != null && !audioSource.isPlaying)
        {
            audioSource.clip = defaultAudioClip;
            audioSource.Play();
        }
    }

    public void StopAudio()
    {
        if (audioSource != null)
        {
            // Check if the audio source is playing and the clip is not the default audio clip
            if (audioSource.isPlaying && audioSource.clip != defaultAudioClip)
            {
                audioSource.Stop();
            }
        }
    }

    public void PlayNewAudio(AudioClip newAudioClip)
    {
        // Stop the current audio before playing the new one
        StopAudio(); 

        if (newAudioClip != null)
        {
            // Play the new audio clip
            audioSource.clip = newAudioClip;
            audioSource.Play();
        }
    }
}
