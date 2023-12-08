using UnityEngine;
using UnityEngine.Audio;

public class SFXVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioSource SFXAudioSource;
    public AudioClip testSFX;

    // Adjust the step value as needed
    public float volumeStep = 2f;

    public void IncreaseVolume()
    {
        float currentVolume;
        audioMixer.GetFloat("volume", out currentVolume);
        // Ensure the volume doesn't exceed 20
        float newVolume = Mathf.Min(20f, currentVolume + volumeStep);
        audioMixer.SetFloat("volume", newVolume);


        // Play the test sound effect when adjusting the SFX volume
        PlayTestSFX();
    }

    public void DecreaseVolume()
    {
        float currentVolume;
        audioMixer.GetFloat("volume", out currentVolume);
        // Ensure the volume doesn't go below 0
        float newVolume = Mathf.Max(-8f, currentVolume - volumeStep);
        audioMixer.SetFloat("volume", newVolume);

        // Play the test sound effect when adjusting the SFX volume
        PlayTestSFX();
    }

    void PlayTestSFX()
    {
        if (SFXAudioSource != null && testSFX != null)
        {
            SFXAudioSource.PlayOneShot(testSFX);
        }
    }
}
