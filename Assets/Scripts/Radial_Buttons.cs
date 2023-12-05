using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radial_Buttons : MonoBehaviour
{
    public ColorManager CM;
    public AudioManager audioManager;

    public AudioSource audioSource;

    public AudioClip redAudioClip;
    public AudioClip blueAudioClip;
    public AudioClip greenAudioClip;
    public AudioClip defaultAudioClip;

    void Start()
    {
        // Ensure the AudioManager is present
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    public void DefaultButton()
    {
        CM.setColorToNormal();
        // Play the default audio clip only if the normal lens has been collected
        if (audioManager != null && CM.HasLensColor(ColorManager.FilterState.Normal))
        {
            audioManager.PlayNewAudio(defaultAudioClip);
        }
    }

    public void RedButton()
    {
        CM.setColorToRed();
        // Play the red audio clip only if the red lens has been collected
        if (audioManager != null && CM.HasLensColor(ColorManager.FilterState.Red))
        {
            audioManager.PlayNewAudio(redAudioClip);
        }
    }

    public void BlueButton()
    {
        CM.setColorToBlue();
        // Play the blue audio clip only if the blue lens has been collected
        if (audioManager != null && CM.HasLensColor(ColorManager.FilterState.Blue))
        {
            audioManager.PlayNewAudio(blueAudioClip);
        }
    }

    public void GreenButton()
    {
        CM.setColorToGreen();
        // Play the green audio clip only if the green lens has been collected
        if (audioManager != null && CM.HasLensColor(ColorManager.FilterState.Green))
        {
            audioManager.PlayNewAudio(greenAudioClip);
        }
    }
}
