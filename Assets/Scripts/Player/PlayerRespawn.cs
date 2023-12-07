using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    public AudioSource fallingSound;

    public FadeEffect screenFade;

    public int maxLives = 3;
    private int currentLives;
    public Slider slide;
    private GameObject lastRespawnPlatform;

    public TextMeshProUGUI livesText;

    void Start()
    {
        currentLives = maxLives;
        screenFade = FindObjectOfType<FadeEffect>();
        UpdateLivesText();
    }

    public void Respawn()
    {
        if (lastRespawnPlatform != null)
        {
            // Respawn above platform
            transform.position = lastRespawnPlatform.transform.position + Vector3.up;
            transform.position += Vector3.up * 6.0f;
            currentLives--;
            slide.value = currentLives;

            // Update the text
            UpdateLivesText();

            if (currentLives <= 0)
            {
                GameOver();
            }
        }
        else
        {
            Debug.LogWarning("No respawn points found with the 'Respawn' tag.");
        }
    }

    public void GameOver()
    {
        // Enable the cursor before loading the GameOver scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("GameOver");
    }

    void UpdateLivesText()
    {
        // Update the UI text with the current number of lives
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }

    public void FallDetected()
    {
        StartCoroutine(FallSequence());
    }

    IEnumerator FallSequence()
    {
        // Play falling sound or other effects
        fallingSound.Play();

        // Fade out
        yield return StartCoroutine(screenFade.FadeOut());

        // Respawn
        Respawn();

        // Fade in
        yield return StartCoroutine(screenFade.FadeIn());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            lastRespawnPlatform = other.gameObject;
        }
    }
}