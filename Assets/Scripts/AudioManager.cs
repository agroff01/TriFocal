using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioClip defaultAudioClip;

    private AudioSource audioSource;

    // Duration of the fade-out and fade-in effects
    public float fadeDuration = 1.0f;

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
                StartCoroutine(FadeOutAndStop());
            }
        }
    }

    public void PlayNewAudio(AudioClip newAudioClip)
    {
        // Start the crossfade
        StartCoroutine(Crossfade(newAudioClip));
    }

    IEnumerator FadeOutAndStop()
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (audioSource.volume > 0)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, 1 - Mathf.Pow(10, Mathf.Log10(1 + elapsedTime) / fadeDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    IEnumerator Crossfade(AudioClip newAudioClip)
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        // Determine the overlap duration
        float overlapDuration = fadeDuration * 0.5f;

        // Play the new audio clip before the old one completely fades out
        if (newAudioClip != null)
        {
            audioSource.clip = newAudioClip;
            audioSource.Play();
        }

        // Continue fading out the old audio while the new one is playing
        while (elapsedTime < fadeDuration)
        {
            float targetVolume = Mathf.Lerp(0f, startVolume, elapsedTime / (fadeDuration - overlapDuration));

            // If the new audio is still playing, adjust its volume as well
            if (audioSource.isPlaying)
            {
                float newAudioVolume = Mathf.Lerp(0f, startVolume, elapsedTime / overlapDuration);
                audioSource.volume = Mathf.Max(targetVolume, newAudioVolume);
            }
            else
            {
                audioSource.volume = targetVolume;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = startVolume;
    }

}
