using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ColorManager colorManager;
    public AudioManager audioManager;
    public Radial_Buttons rad;

    public AudioClip finalAudioClip;

    void Start()
    {
        // Ensure the ColorManager is present
        if (colorManager == null)
        {
            colorManager = FindObjectOfType<ColorManager>();
        }

        // Ensure the AudioManager is present
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        if (rad == null)
        {
            rad = FindObjectOfType<Radial_Buttons>();
        }
    }

    void Update()
    {
        if (rad.CheckInteraction() == false)
        {
            if (colorManager.AllLensesCollected())
            {
                PlaySpecialAudio();
            }
        }
        
    }

    private void PlaySpecialAudio()
    {
        if (audioManager != null && finalAudioClip != null)
        {
            audioManager.PlayNewAudio(finalAudioClip);
        }
    }
}