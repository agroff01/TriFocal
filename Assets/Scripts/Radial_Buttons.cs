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

    private AudioClip currentAudioClip;

    private bool canInteract = true;

    void Start()
    {
        // Ensure the AudioManager is present
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    void Update()
    {
        // Check for left mouse button click
        if (canInteract && Input.GetMouseButtonDown(0))
        {
            // Perform button functionality based on mouse position
            HandleMouseInput();
        }
    }

    private void HandleMouseInput()
    {
        // Raycast to check which button is clicked
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check which button is clicked based on the collider or tag
            if (hit.collider.CompareTag("RedButton"))
            {
                RedButton();
            }
            else if (hit.collider.CompareTag("BlueButton"))
            {
                BlueButton();
            }
            else if (hit.collider.CompareTag("GreenButton"))
            {
                GreenButton();
            }
            else if (hit.collider.CompareTag("DefaultButton"))
            {
                DefaultButton();
            }
        }
    }

    public bool CheckInteraction()
    {
        // Check if all lenses are collected, disable interaction if true
        if (CM.AllLensesCollected())
        {
            canInteract = false;
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Pause();
            }
            

        }
        return canInteract;
    }

    public void DefaultButton()
    {
        CM.setColorToNormal();
        // Play the default audio clip only if the normal lens has been collected and audio interaction is allowed
        if (audioManager != null && CM.HasLensColor(ColorManager.FilterState.Normal) && currentAudioClip != defaultAudioClip && canInteract == true)
        {
            audioManager.PlayNewAudio(defaultAudioClip);
            currentAudioClip = defaultAudioClip;
        }
    }

    public void RedButton()
    {
        CM.setColorToRed();
        // Play the red audio clip only if the red lens has been collected and audio interaction is allowed
        if (audioManager != null && CM.HasLensColor(ColorManager.FilterState.Red) && currentAudioClip != redAudioClip && canInteract == true)
        {
            audioManager.PlayNewAudio(redAudioClip);
            currentAudioClip = redAudioClip;
        }
    }

    public void BlueButton()
    {
        CM.setColorToBlue();
        // Play the blue audio clip only if the blue lens has been collected and audio interaction is allowed
        if (audioManager != null && CM.HasLensColor(ColorManager.FilterState.Blue) && currentAudioClip != blueAudioClip && canInteract == true)
        {
            audioManager.PlayNewAudio(blueAudioClip);
            currentAudioClip = blueAudioClip;
        }
    }

    public void GreenButton()
    {
        CM.setColorToGreen();
        // Play the green audio clip only if the green lens has been collected and audio interaction is allowed
        if (audioManager != null && CM.HasLensColor(ColorManager.FilterState.Green) && currentAudioClip != greenAudioClip && canInteract == true)
        {
            audioManager.PlayNewAudio(greenAudioClip);
            currentAudioClip = greenAudioClip;
        }
    }
}
